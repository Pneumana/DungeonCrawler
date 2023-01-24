using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class PlaceRingComponent : MonoBehaviour
    {
        public GameObject player;
        //these two variables determine where the object is placed
        public float SpawnAngle;
        public float SpawnDistance;
        //runnable when the player touches the "refresh board" volume
        public void PlaceMe(float x, float y, float z, float distance, float angle)
        {
            this.gameObject.transform.position = new Vector3(x - (Mathf.Sin(angle) * distance), y + (Mathf.Cos(angle) * distance), z);
            
        }
        // places the part at start
        void Start()
        {
            this.PlaceMe(0.0f, 37.0f, 0.0f, 13.0f, SpawnAngle);
        }
    }
}
