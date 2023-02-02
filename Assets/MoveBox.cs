using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

    public class MoveBox : MonoBehaviour
    {
    public bool down;
    public float speed = 1.0f;
    public float maxDistance = 3.0f;
    Vector3 pos = Vector3.zero;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        pos = this.gameObject.transform.position;
        if (down == false)
        {
            this.gameObject.transform.position = new Vector3(pos.x, pos.y + (speed * Time.deltaTime));
            Debug.Log("moving up");
        }
        if (down == true)
        {
            this.gameObject.transform.position = new Vector3(pos.x, pos.y - (speed * Time.deltaTime));
            Debug.Log("moving down");
        }
        //moves up and down until reaching the maxDistance float
        if(pos.y > maxDistance)
        {
            down = true;
        }
        if(pos.y < -maxDistance)
        {
            down = false;
        }
        }
    }

