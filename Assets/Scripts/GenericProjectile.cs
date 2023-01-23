using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class GenericProjectile : MonoBehaviour
    {
        Rigidbody2D body;
        public float speed;
        public GameObject player;
        //add piereces as a stat


        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            body.AddForce(transform.up * speed, ForceMode2D.Impulse);
            this.gameObject.transform.localScale = new Vector3(1.0f + (player.GetComponent<Move>().Area * 0.1f),1.0f + (player.GetComponent<Move>().Area * 0.1f));
            Debug.Log("staring movement");
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "Enemy")
            {
                //Debug.Log("Hit enemy " + collision.gameObject.name + " for " + (int)((plr.GetComponent<Move>().Damage + 3) * 1.5) + " damage");
            }

        }
    }
}
