using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class DamageNumbers : MonoBehaviour
    {
        public float age;
        public int rotation;
        public int Number;
        public bool isCrit;
        // Start is called before the first frame update
        void Start()
        {
        rotation = UnityEngine.Random.Range(-10, 10);
            if(Number >= 10)
            {
                this.GetComponent<TextMeshPro>().color = new Color(1, 1, 0);
            }
            if (Number >= 20)
            {
                this.GetComponent<TextMeshPro>().color = new Color(1, 0.5f, 0);
            }
            this.GetComponent<TextMeshPro>().text = Number.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            age += 1.0f * Time.deltaTime;
            if (age > 1.5f)
            {
                Destroy(gameObject);
            }
            transform.localScale = new Vector3(Mathf.Sin(age*2),Mathf.Sin(age * 2));
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }
    }
}
