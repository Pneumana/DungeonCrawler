using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerUpgradeUI : MonoBehaviour
{
    public UIUpdater ui;
    public bool isSpecial;
    // Start is called before the first frame update
    void Start()
    {
        if (isSpecial) { gameObject.GetComponent<SpriteRenderer>().color = new Color(0.95294117647f, 0.72156862745f, 0.09411764705f); }
        ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            
            if (isSpecial == false)
            {
                //ui.enemynumber.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
                Debug.Log("Upgrades are displayed");
                ui.SpawnUpgrades();
            }
            else if (isSpecial == true && ui.pickeUpgrades.Count < 3)
            {
                ui.SpawnSpecialUpgrades();
            }
        }
        Kill();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void Kill()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
