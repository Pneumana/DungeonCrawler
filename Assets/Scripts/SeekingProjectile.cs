using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class SeekingProjectile : MonoBehaviour
    {
        public Transform target;
        public float targetAngle;

        public float speed;
        public float pierces;
        public float lifespan;
        public float maxLifespan;

        public bool isEnemy = false;
        public float anglevalue;

        // Start is called before the first frame update
        void Start()
        {
            if (isEnemy == false)
            {
                GetClosestEnemy();
            }
            else 
            {
                target = GameObject.Find("Player").transform;
            }
        }

        void GetClosestEnemy()
        {
            float current = 99999f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                target = GameObject.Find("Player").transform;
            }
            else
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    //float distance = Mathf.Abs((enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y));
                    float distance = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
                    if (enemy.GetComponent<EnemyBody>() != null)
                    {
                        //target = enemy.transform;
                    }
                    if (distance < current)
                    {
                        Debug.Log("new closest " + enemy.name + " with distance of " + distance);
                        current = distance;
                        target = enemy.transform;
                    }
                }
            }
        }
        private void Kill()
        {
                Destroy(this.gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && !isEnemy)
            {
                Kill();
            }
            if (collision.gameObject.tag == "Player" && isEnemy)
            {
                Kill();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && !isEnemy)
            {
                Kill();
            }
            if (collision.gameObject.tag == "Player" && isEnemy)
            {
                Kill();
            }
        }
        // Update is called once per frame
        void Update()
        {
        
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (target == null)
            {
                GetClosestEnemy();
            }
            else
            {
                float rot = (Mathf.Atan2(target.position.x - transform.position.x, target.position.y - transform.position.y) * Mathf.Rad2Deg);
                /*float rotSpeed = 5f;

                if (anglevalue < rot)
                {
                    anglevalue += rotSpeed * Time.deltaTime;
                }
                if (anglevalue > rot)
                {
                    anglevalue -= rotSpeed * Time.deltaTime;
                }
                if(anglevalue > 100f && rot < 0)
                {
                    anglevalue = 179f ;
                }
                if (anglevalue < -100f && rot > 0)
                {
                    anglevalue = -179f ;
                }*/
                transform.GetComponent<Rigidbody2D>().rotation = -rot;
                //prevent the infinate looping.
                /* if(transform.GetComponent<Rigidbody2D>().rotation < 0 && rot > 0)
                 {
                     Debug.Log("Matching rotations via +");
                     transform.GetComponent<Rigidbody2D>().rotation += 360;
                 }
                 if (transform.GetComponent<Rigidbody2D>().rotation > 0 && rot < 0)
                 {
                     Debug.Log("Matching rotations via -");
                     transform.GetComponent<Rigidbody2D>().rotation -= 360;
                 }
                 //rotate
                 if (transform.GetComponent<Rigidbody2D>().rotation + 1f > -rot)
                 {
                     transform.GetComponent<Rigidbody2D>().rotation -= 180.0f * Time.deltaTime;
                 }
                 if (transform.GetComponent<Rigidbody2D>().rotation - 1.0f < -rot)
                 {
                     transform.GetComponent<Rigidbody2D>().rotation += 180.0f * Time.deltaTime;
                 }*/
                //Debug.Log("target rot: " + rot + " rot: " + transform.GetComponent<Rigidbody2D>().rotation);
            }
            //float rot = (Mathf.Atan2(target.position.x - transform.position.x, target.position.y - transform.position.y) * Mathf.Rad2Deg);

            //new look code 


            transform.GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        }
    }
}
