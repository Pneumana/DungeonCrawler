using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class Mover : MonoBehaviour
    {
        // Start is called before the first frame update
        int speed;
        void Start()
        {
            speed = 10;
            Debug.Log("light speed baby: " + speed);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W)) 
            {
            this.gameObject.transform.position = this.gameObject.transform.position + (Vector3.up * Time.deltaTime) * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.position = this.gameObject.transform.position - (Vector3.up * Time.deltaTime) * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.position = this.gameObject.transform.position + (Vector3.right * Time.deltaTime) * speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.position = this.gameObject.transform.position - (Vector3.right * Time.deltaTime) * speed;
            }
        }
    }
}
