using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class KillParticleEmitter : MonoBehaviour
    {
        public float Lifespan;
        public float MaxLifespan;

        // Update is called once per frame
        void Update()
        {
            if (Lifespan <= MaxLifespan)
            {
                Lifespan += 1.0f * Time.deltaTime;
            }
            if (Lifespan >= MaxLifespan)
            {
                Destroy(gameObject);
            }
        }
    }
}
