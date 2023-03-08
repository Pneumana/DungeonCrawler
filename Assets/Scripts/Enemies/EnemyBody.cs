using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
            player = GameObject.Find("Player").GetComponent<Move>();
            dWSprite = Resources.Load("Textures/deepwounds") as Sprite;
            basicUpgrades = GameObject.Find("Weird Particles").transform.Find("TriggerUpgrade").transform.gameObject;
            SetStats();
        }
        void SetStats()
        {
            //multiply hpPerLevel by the wave/level number
            health = maxHealth + (hpPerLevel * ui.freePlayWaves);
        }
        // Update is called once per frame
        void Update()
        {
            dist2Player = Vector2.Distance(player.gameObject.transform.position, gameObject.transform.position);
            if (health <= 0) { Kill(); }
            if (dist2Player <=  5 + player.Area * 1.25 && second >= 1f && player.empowerDuration > 0)
            {
                TakeDamage(3);
                second = 0;
            }
            if(second < 1)
            {
                second += 1.0f * Time.deltaTime;
            }
            //THIS IS DEBUG
            /*if (Input.GetKeyDown(KeyCode.Mouse2)) {
                GameObject spawnme = GameObject.Find("Weird Particles").transform.Find("TriggerUpgrade").transform.gameObject;
                GameObject newUpgrade;
                newUpgrade = Instantiate(spawnme);
                newUpgrade.transform.position = transform.position;
                newUpgrade.SetActive(true);
                Kill();
            }*/

            //update deepwounds debuff
            if (deepWounds > 0 && !debounce) { debounce = true; CreateDeepWoundIcon(); }
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
            GameObject spawnme;
            //
            spawnme = GameObject.Find("Weird Particles").transform.Find("TriggerUpgrade").transform.gameObject;
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
                newUpgrade = Instantiate(spawnme);
                newUpgrade.transform.position = startingpos;
                newUpgrade.SetActive(true);
                //player.TakeDamage(-1, 0);
                ui.waveCount += 1;
                if (ui.waveCount > 0)
                {
                    Debug.Log("Starting wave " + ui.waveCount);
                    //gives you 3 normal upgrades before you get a special
                    if(ui.waveCount >= 16 && !player.freeplay)
                    {
                        Debug.Log("Player won! Hooray!");
                        ui.WinGame();
                    }
                    else if (ui.waveCount % 4 == 0 && !player.freeplay)
                    {
                        newUpgrade.GetComponent<TriggerUpgradeUI>().isSpecial = true;
                    }
                    if (player.freeplay) { ui.freePlayWaves += 1; }
                }
            }
            if (player.Health <= 1 && player.Siphon > 0) { player.Health += 1; }
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
                    Debug.Log(child.name + ", " + enemyType + "Gib" );
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
                if (ui.pickeUpgrades.Contains(4)) { TakeDamage(2, true); }
                isBonus = true;
                
            }
            if(this.gameObject.GetComponent<SniperAI>() != null)
            {
                this.gameObject.GetComponent<SniperAI>().abilityCD = 1;
                this.gameObject.GetComponent<SniperAI>().shotCharge = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && health > 0)
            {
                //create damage numbers
                
                if (collision.name != "SpecialFireStaff(Clone)" && collision.name != "BasicFireStaff(Clone)" && collision.name != "Fireball(Clone)" && collision.name != "BasicBackswingFireStaff(Clone)")
                {
                    TakeDamage(player.Damage);
                }

                
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //checks if the player's objects are touching the enemy
            if(collision.gameObject.tag == "Player" && health > 0)
            {
                //create damage numbers

                if (collision.gameObject.name == "Parried(Clone)")
                {
                    TakeDamage(player.Damage * 3);
                }
                if (collision.gameObject.name != "SpecialFireStaff(Clone)" && collision.gameObject.name != "BasicFireStaff(Clone)" && collision.gameObject.name != "Fireball(Clone)" && collision.gameObject.name != "BasicBackswingFireStaff(Clone)" && collision.gameObject.name != "Parried(Clone)")
                {
                    TakeDamage(player.Damage);
                }
                if (collision.gameObject.name == "BasicFireStaff(Clone)" || collision.gameObject.name != "BasicBackswingFireStaff(Clone)" && collision.gameObject.name != "Fireball(Clone)" && player.EquippedWeapon != "Sword")
                {
                    Debug.Log("Hit with firestaff");
                    TakeDamage((int)Mathf.Ceil(player.Damage * 0.66f));
                }
                if (collision.gameObject.name == "Fireball(Clone)")
                {
                    Debug.Log("Hit with projectile from firestaff");
                    TakeDamage((int)Mathf.Ceil(player.Damage * 0.33f));
                }
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }
        
    }
}
