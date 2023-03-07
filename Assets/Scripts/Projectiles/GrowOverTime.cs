using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class GrowOverTime : MonoBehaviour
    {
    // Start is called before the first frame update
    public GameObject player;
        private float age = 0;
        //base size of the hitbox
        public float baseScale = 1.0f;
        //increase in size per second
        public float scaleFactor = 0.1f;
        //used for damage ticks
        //deals damage over 0.25 second intervals
        private float damageCD = 0;
        public bool isEnemy = false;
    public bool doTicks = false;
    public GameObject enemy;
        void Start()
        {
        player = GameObject.Find("Player");
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<EnemyBody>() != null)
        {
            enemy = collision.gameObject;

            doTicks = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<EnemyBody>() != null)
        {
            enemy = collision.gameObject;
            doTicks = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        doTicks = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        doTicks = false;
    }
    // Update is called once per frame
    void Update()
        {
            age += 1 * Time.deltaTime;
        damageCD -= 1.0f * Time.deltaTime;
            this.gameObject.transform.localScale =  new Vector3(baseScale + ((scaleFactor * age) * (player.gameObject.GetComponent<Move>().Area + 1)), baseScale + (scaleFactor * age) * (player.gameObject.GetComponent<Move>().Area + 1));
            if (age >= 5)
            {
                Destroy(this.gameObject);
            }
            if (isEnemy == false)
            {
                //Debug.Log("friend :D");
            }
            if(damageCD <= 0 && doTicks)
            {
            Debug.Log("Log my nuts");
                enemy.gameObject.GetComponent<EnemyBody>().TakeDamage(player.GetComponent<Move>().Damage - 2);
                damageCD = 0.25f;
            }
            if (isEnemy == true)
            {
                //Debug.Log("devil D:");
            }
    }
    }

