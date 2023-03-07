using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class BarrierWallBehavior : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Refresh();
        }
        public void Refresh()
        {
            float rot = this.gameObject.GetComponent<PlaceRingComponent>().SpawnAngle;
            float distance = this.gameObject.GetComponent<PlaceRingComponent>().SpawnDistance;
            transform.localScale = new Vector3(1.0f * distance, 1.0f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
            
            var shape = transform.Find("Firewall").GetComponent<ParticleSystem>().shape;
            shape.scale = transform.localScale;
            var emit = transform.Find("Firewall").GetComponent<ParticleSystem>().emission;
            emit.rateOverTime = Mathf.Min(10 * distance, transform.Find("Firewall").GetComponent<ParticleSystem>().main.maxParticles);
        
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
