using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class EnemyBody : MonoBehaviour
    {
        //Script Scope: this piece is the body of the enemy, letting it take damage and spawn particle effects.
        //AI and movement is set in a different script.
        public float health;
        public float maxHealth;
        public float damage;
        //used to increase stats by an amount
        public float hpPerLevel;
        public float spdPerLevel;
        public float dmgPerLevel;

        public string[] effects;

        public string enemyType;

        public UIUpdater ui;
        public Move player;

        public float speed;
        public float dist2Player;

        public float second;

        public int deepWounds = 0;
        bool debounce;
        Sprite dWSprite;
        GameObject debuff;
        public GameObject basicUpgrades;
        // Start is called before the first frame update
        void Start()
        {
            SetStats();
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
            player = GameObject.Find("Player").GetComponent<Move>();
            dWSprite = Resources.Load("Textures/deepwounds") as Sprite;
            basicUpgrades = GameObject.Find("Weird Particles").transform.Find("TriggerUpgrade").transform.gameObject;
        }
        void SetStats()
        {
            //multiply hpPerLevel by the wave/level number
            health = maxHealth + (hpPerLevel);
        }
        // Update is called once per frame
        void Update()
        {
            dist2Player = Vector2.Distance(player.gameObject.transform.position, gameObject.transform.position);
            if (health <= 0) { Kill(); }
            if (dist2Player <=  5 + player.Area * 1.25 && second >= 1f && player.empowerDuration > 0)
            {
                TakeDamage(1);
                second = 0;
            }
            if(second < 1)
            {
                second += 1.0f * Time.deltaTime;
            }
            //update deepwounds debuff
            
            if(deepWounds > 0 && !debounce) { debounce = true; CreateDeepWoundIcon(); }
        }
       void CreateDeepWoundIcon()
        {
            debuff = Instantiate(GameObject.Find("Weird Particles").gameObject.transform.Find("deepwound").gameObject);
            debuff.SetActive(true);
            debuff.transform.parent = this.gameObject.transform;
            debuff.transform.position = gameObject.transform.position;
        }
        void Kill()
        {
            var startingpos = this.gameObject.transform.position;
            Destroy(gameObject);
            //do other on kill effects here.
            if (ui.pickeUpgrades.Contains(0))
            {
                //spawn death seeker
                GameObject seeker;
                seeker = Instantiate(GameObject.Find("ProjectileStorage").transform.Find("FromUpgrades").transform.Find("DeathSeeker").gameObject);
                seeker.SetActive(true);
                seeker.transform.position = startingpos;
            }
            //grants empower for X seconds
            if (ui.pickeUpgrades.Contains(3))
            {
                player.empowerDuration += 3f + (player.Duration * 1.0f);
            }
            //spawn upgrades if this is the last enemy
            if (ui.enemyTotal == 1 || ui.enemyTotal == 0)
            {
                GameObject newUpgrade;
                newUpgrade = Instantiate(basicUpgrades);
                newUpgrade.transform.position = startingpos;
                newUpgrade.SetActive(true);
                if (ui.waveCount > 0)
                {
                    if (ui.waveCount % 3 == 0)
                    {
                        newUpgrade.GetComponent<SpawnUpgrade>().isSpecial = true;
                    }
                }
            }
            //roll for siphon healing here

            //spawn corpse here too
            ui.UpdateEnemyNumber();
        }

        public void TakeDamage(int damage, bool isBonus = false)
        {
            
            for (int i = 0; i < GameObject.Find("Weird Particles").gameObject.transform.childCount; i++)
            {
                GameObject child = GameObject.Find("Weird Particles").gameObject.transform.GetChild(i).gameObject;
                if (child.name == "DamageNumber")
                {
                    GameObject damageNumber;
                    damageNumber = Instantiate(child);
                    damageNumber.gameObject.SetActive(true);
                    damageNumber.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f), -1.0f);
                    if (ui.pickeUpgrades.Contains(5))
                    {
                        damage += 2 * deepWounds;
                    }
                        //this part can be changed to a value on the projectiles themselves
                        //include any random rolls where it sets damage
                    damageNumber.GetComponent<DamageNumbers>().Number = damage;
                    health -= damage;
                }
                //searches for enemyTypeGib (slime)
                if (child.name == enemyType + "Gib")
                {
                    GameObject newparticle;
                    newparticle = Instantiate(child);
                    newparticle.gameObject.SetActive(true);
                    newparticle.transform.position = transform.position;
                    newparticle.transform.rotation = Quaternion.Euler(Random.Range(-180, 180), -90, 90);

                    ParticleSystem gib;
                    gib = newparticle.GetComponent<ParticleSystem>();
                    gib.Play();
                    var on = gib.emission;
                    on.enabled = true;

                }

            }
            if (ui.pickeUpgrades.Contains(5)) { deepWounds += 1; }
            if (isBonus == false)
            {
                if (ui.pickeUpgrades.Contains(4)) { TakeDamage(1, true); }
                isBonus = true;
                
            }
            if(this.gameObject.GetComponent<SniperAI>() != null)
            {
                this.gameObject.GetComponent<SniperAI>().abilityCD = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && health > 0)
            {
                //create damage numbers
                TakeDamage(player.Damage);
                
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //checks if the player's objects are touching the enemy
            if(collision.gameObject.tag == "Player" && health > 0)
            {
                //create damage numbers
                TakeDamage(player.Damage);
                
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }
        
    }
}
