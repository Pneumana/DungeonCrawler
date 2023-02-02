using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class GrowOverTime : MonoBehaviour
    {
        // Start is called before the first frame update
       
        private int age = 0;
        //base size of the hitbox
        public float baseScale = 1.0f;
        //increase in size per second
        public float scaleFactor = 0.1f;
        //used for damage ticks
        //deals damage over 0.25 second intervals
        private int damageCD = 0;
        public bool isEnemy = false;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            age += 1;
            this.gameObject.transform.localScale =  new Vector3(this.gameObject.transform.localScale.x + (0.1f * Time.deltaTime), this.gameObject.transform.localScale.y + (0.1f * Time.deltaTime));
            if (age >= 120)
            {
                Destroy(this.gameObject);
            }
            if (isEnemy == false)
            {
                Debug.Log("friend :D");
            }
            if (isEnemy == true)
            {
                Debug.Log("devil D:");
            }
    }
    }

