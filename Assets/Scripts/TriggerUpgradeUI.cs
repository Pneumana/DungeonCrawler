using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class TriggerUpgradeUI : MonoBehaviour
    {
        public UIUpdater ui;
        public bool isSpecial;
        // Start is called before the first frame update
        void Start()
        {
            if (isSpecial) { gameObject.GetComponent<SpriteRenderer>().color = new Color(0.95294117647f, 0.72156862745f, 0.09411764705f); }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                if (isSpecial == true && ui.pickeUpgrades.Count < 3) 
                {
                    ui.SpawnSpecialUpgrades();
                }
                if (isSpecial == false)
                {
                    ui.SpawnUpgrades();
                }
                Destroy(this.gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
