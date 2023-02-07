using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class EnemyBody : MonoBehaviour
    {
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
            if(collision.gameObject.tag == "Player" && health > 0)
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
                        //this part can be changed to a value on the projectiles themselves
                        //include any random rolls where it sets damage
                        var damage = GameObject.Find("Player").GetComponent<Move>().Damage;
                        damageNumber.GetComponent<DamageNumbers>().Number = damage;
                        health -= damage;
                    }

                }
                //Debug.Log("Hit Enemy for " + collision.gameObject.GetComponent<Move>().Damage + 1);
            }
        }
        
    }
}
