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
            //other keybinds
            if (Input.GetKey(KeyCode.Q)) { Debug.Log("Q"); }
            if (Input.GetKey(KeyCode.A)) { Debug.Log("A"); }
            if (Input.GetKey(KeyCode.W)) { Debug.Log("W"); }

            if (Input.GetKey(KeyCode.R)) { Debug.Log("R"); }
            if (Input.GetKey(KeyCode.T)) { Debug.Log("T"); }
            if (Input.GetKey(KeyCode.G)) { Debug.Log("G"); }
            //movement
            float up = 0;
            float right = 0;
            if (Input.GetKey(KeyCode.E)) 
            {
                up = 1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                up = -1.0f;
            }
            if (Input.GetKey(KeyCode.F))
            {
                right = 1.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                right = -1.0f;
            }
            if(right != 0 && up != 0) 
            {
                right *= 0.75f;
                up *= 0.75f;
                Debug.Log("Moving diagonally");
            }
            this.gameObject.transform.position += new Vector3((right * Time.deltaTime) * speed, (up * Time.deltaTime) * speed);
        }
    }
}
