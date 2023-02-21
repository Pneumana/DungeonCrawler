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
        public float SpawnAngleOffset;

        public string biome;
        //runnable when the player touches the "refresh board" volume
        public void PlaceMe(float x, float y, float z, float distance, float angle)
        {
            
            if (biome == "lush") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().lushAngle; }
            else if(biome == "barren") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().barrenAngle; }
            else if (biome == "ruins") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().ruinsAngle; }
            //if the object has a barrier wall piece, refresh it
            if (gameObject.GetComponent<BarrierWallBehavior>() != null) { gameObject.GetComponent<BarrierWallBehavior>().Refresh(); z = -4.0f;  }
            this.gameObject.transform.position = new Vector3((Mathf.Floor(x - (Mathf.Sin(angle) * distance))) +0.5f, (Mathf.Floor(y + (Mathf.Cos(angle) * distance))) + 0.5f, z);
        }
        // places the part at start
        void Start()
        {
            //mathf.PI/180 converts the spawn angle to radians
            this.PlaceMe(0.0f, 37.0f, 0.0f, SpawnDistance, SpawnAngle * (Mathf.PI/180));
        }

        private void Update()
        {
            //SpawnAngle += 1.0f * Time.deltaTime;
            //this.PlaceMe(0.0f, 37.0f, 0.0f, SpawnDistance, SpawnAngle * (Mathf.PI / 180));
        }
    }
}
