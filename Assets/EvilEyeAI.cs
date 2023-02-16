using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

namespace UnityEngine.Localization
{
    public class EvilEyeAI : MonoBehaviour
    {
        private EnemyBody body;

        public float speed = 5;

        private bool blinded = false;

        public float lookrot;

        private Vector3 playerPos;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<EnemyBody>();
        }

        void CheckEffects()
        {
            for (int i = 0; i < body.effects.Length; i++)
            {
                if (body.effects[i] == "blind")
                {
                    Debug.Log("Enemy is blind");
                    blinded = true;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (blinded == false)
            {
                playerPos = GameObject.Find("Player").transform.position;
            }
            //look at the player's last seen location
            float rot = (Mathf.Atan2(playerPos.x - transform.position.x, playerPos.y - transform.position.y) * Mathf.Rad2Deg) ;
            
            if (transform.GetComponent<Rigidbody2D>().rotation + 1 > -rot)
            {
                transform.GetComponent<Rigidbody2D>().rotation -= 180.0f * Time.deltaTime;
            }
            if (transform.GetComponent<Rigidbody2D>().rotation - 1 < -rot)
            {
                transform.GetComponent<Rigidbody2D>().rotation += 180.0f * Time.deltaTime;
            }
            transform.GetComponent<Rigidbody2D>().rotation = -rot;
            //Adding a prequisite to make the AI spin in place might make them look a bit less janky.

            //move up
            //respects wall collisions
            transform.GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
            //does not respect wall collisions
            //body.transform.position += transform.up * (speed * Time.deltaTime);

            lookrot = -rot;
        }
    }
}
