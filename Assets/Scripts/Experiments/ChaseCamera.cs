using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class ChaseCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject target;
        float destX;
        float destY;
        float distance;

        float speed;
        void Start()
        {
            destX = target.transform.position.x;
            destY = target.transform.position.y;
            //place the camera on top of the target at the start
            this.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }

        // Update is called once per frame
        void Update()
        {
            destX = target.transform.position.x;
            destY = target.transform.position.y;
            distance = (destX - this.gameObject.transform.position.x) / (destY - this.gameObject.transform.position.y);
            if (distance < 2) { speed = 0; }
            if (distance >= 2) { speed = 1; }

            if (destX > this.gameObject.transform.position.x)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }
}
