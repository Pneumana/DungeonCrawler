using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEngine.Localization
{
    public class DebugBiomeAngles : MonoBehaviour
    {
        public GameObject player;
        //these two variables determine where the object is placed
        public float SpawnAngle;
        public float SpawnDistance;
        public float SpawnAngleOffset;

        public string biome;
        // Start is called before the first frame update
        void Start()
        {
        
        }
        public void PlaceMe(float x, float y, float z, float distance, float angle)
        {

            if (biome == "lush") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().lushAngle; }
            else if (biome == "barren") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().barrenAngle; }
            else if (biome == "ruins") { SpawnAngle = GameObject.Find("Biomes").GetComponent<SetBiomeLocations>().ruinsAngle; }
            //if the object has a barrier wall piece, refresh it
            if (gameObject.GetComponent<BarrierWallBehavior>() != null) { gameObject.GetComponent<BarrierWallBehavior>().Refresh(); z = -4.0f; }
            z = -4.0f;
            this.gameObject.transform.position = new Vector3(Mathf.Floor(x - (Mathf.Sin(angle) * distance)), Mathf.Floor(y + (Mathf.Cos(angle) * distance)), z);
            this.gameObject.transform.rotation =  Quaternion.Euler(new Vector3(0, 0, (angle/ (Mathf.PI / 180)) + 90));
        }
        // Update is called once per frame
        void Update()
        {
            this.PlaceMe(0.0f, 37.0f, 0.0f, SpawnDistance, SpawnAngle * (Mathf.PI / 180));
        }
    }
}
