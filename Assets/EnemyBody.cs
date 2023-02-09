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

        public string enemyType;

        public float speed;
        // Start is called before the first frame update
        void Start()
        {
            SetStats();
        }
        void SetStats()
        {
            //multiply hpPerLevel by the wave/level number
            health = maxHealth + (hpPerLevel);
        }
        // Update is called once per frame
        void Update()
        {
        if(health <= 0) { Kill(); }
        }
        void Kill()
        {
            //do other on kill effects here.

            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //calculate the angle at which the damage was recieved
            var impactRot = Mathf.Rad2Deg * (collision.gameObject.transform.rotation.z);
            Debug.Log(impactRot);
            //checks if the player's objects are touching the enemy
            if(collision.gameObject.tag == "Player" && health > 0)
            {
                if (enemyType == "Slime")
                {

                }
                //create damage numbers
                for (int i = 0; i < GameObject.Find("Weird Particles").gameObject.transform.childCount; i++)
                {
                    GameObject child = GameObject.Find("Weird Particles").gameObject.transform.GetChild(i).gameObject;
                    if (child.name == "DamageNumber")
                    {
                        GameObject damageNumber;
                        damageNumber = Instantiate(child);
                        damageNumber.gameObject.SetActive(true);
                        damageNumber.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f), -1.0f);
                        
                        //this part can be changed to a value on the projectiles themselves
                        //include any random rolls where it sets damage
                        var damage = GameObject.Find("Player").GetComponent<Move>().Damage;
                        damageNumber.GetComponent<DamageNumbers>().Number = damage;
                        health -= damage;
                    }
                    //searches for enemyTypeGib (slime)
                    if(child.name == enemyType + "Gib") 
                    {
                        ParticleSystem gib;
                        gib = child.GetComponent<ParticleSystem>();
                        gib.Stop();
                        child.transform.position = transform.position;
                        child.transform.rotation = Quaternion.Euler(impactRot + 180, -90, 90);
                        gib.Play();
                    }
                    
                }
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }
        
    }
}
