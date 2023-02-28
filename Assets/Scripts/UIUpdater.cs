using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    public GameObject greyOut;
    public GameObject swordMask;
    public GameObject Player;

    GameObject healthbar;
    GameObject healthnumber;
    GameObject enemynumber;
    public int enemyTotal;

    public int waveCount;

    public GameObject card;

    public List<int> numbers = new List<int>();
    public List<int> pickeUpgrades = new List<int>();
    public Sprite altCard;
    public int count = 3;
    public int maxRange = 5;

    public bool reUpdate;
    //public GameObject[] enemies;

    public float shakeAmp = 0;
    public float shakeTime  =0;
    float xOffset;
    // Start is called before the first frame update
    void Start()
    {
        xOffset = greyOut.transform.position.x;
        healthbar = GameObject.Find("HealthBar");
        healthnumber = GameObject.Find("HealthNumber");
        enemynumber = GameObject.Find("EnemyNumber");
        //use this function for when the player interracts with the upgrader
        //SpawnUpgrades();
        UpdateEnemyNumber();
    }
    public void UpdateSword()
    {
      
    }

    public void UpdateEnemyNumber(bool retry = true)
    {
        
        GameObject[] enemies;
        Debug.Log("new array");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int total = 0;
        foreach(GameObject enemy in enemies)
        {
            if(enemy.name != "Missing (Game Object)") 
            {
                total += 1;
            }
            Debug.Log(enemy.name);

        }
        //include case for if there ar eno enemies
        if (total == 1)
        {
            enemynumber.GetComponent<TextMeshProUGUI>().text = total.ToString() + " Enemy Remaining";
        }
        else 
        {
            enemynumber.GetComponent<TextMeshProUGUI>().text = total.ToString() + " Enemies Remaining";
        }
        enemyTotal = total;
        if(retry == true) { reUpdate = true; }
        
    }

    public void UpdateHealth()
    {
        var player = Player.GetComponent<Move>();
        healthnumber.GetComponent<TextMeshProUGUI>().text = player.Health.ToString();
        healthbar.transform.localScale = new Vector3(5.0f * (float)((float)player.Health / (float)player.MaxHealth), 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        UpdateHealth();
        if (reUpdate == true) { UpdateEnemyNumber(false); reUpdate = false; }
        //Debug.Log("Desired position : " + (Player.GetComponent<Move>().charge/6.25f) * 100);
        if (Player.GetComponent<Move>().hasSword == true)
        {
            //Debug.Log("has sword");
            greyOut.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (Player.GetComponent<Move>().hasSword == false)
        {
            //Debug.Log("lost sword :(");
            greyOut.GetComponent<Image>().color = new Color32(140, 140, 140, 255);
        }
        if (Player.GetComponent<Move>().charge >= Player.GetComponent<Move>().chargeMax)
        {
            if (shakeAmp < 10)
            {
                shakeAmp += 3.3f * Time.deltaTime;
            }
            shakeTime += 0.1f;
            greyOut.transform.position = new Vector3((Mathf.Sin(shakeTime) * shakeAmp) + xOffset, greyOut.transform.position.y, 0.0f);
                
        }
        if (Player.GetComponent<Move>().charge == 0)
        {
            shakeAmp = 0;
            shakeTime = 0;
            greyOut.transform.position = new Vector3(xOffset, greyOut.transform.position.y, 0.0f);
        }
        pos = greyOut.transform.position;
        swordMask.transform.position = new Vector3(pos.x, greyOut.transform.position.y + (Mathf.Min(((Player.GetComponent<Move>().charge / Player.GetComponent<Move>().chargeMax) * 50.0f), 50.0f) - 50.0f), 0.0f);
    }

    public void SpawnUpgrades()
    {
        GameObject clone;
        //creates 3 UpgradeCard objects, positioning them from left to right
        // in this function, add results and conditonals for new rolls not == results
        string result1;
        string result2;
        string result0;

        numbers.Clear();
        //generate new numbers :D
        for (int i = 0; i < count; i++)
        {
            //maxRange should be the length of the UpgradeCardBehaviors listUpgradeNames[]
            int temp = Random.Range(0, 7);
            while (numbers.Contains(temp))
            {
                temp = Random.Range(0, 7);
            }

            numbers.Add(temp);

        }

        string result;
        for (int i = 0; i < 3; i++)
        {
            //set the upgradeID of each card to numbers[i]
            GameObject desc;
            GameObject cardname;
            clone = Instantiate(card, transform);
            clone.SetActive(true);
            clone.transform.SetParent(GameObject.Find("Canvas").gameObject.transform, false);
            clone.transform.position = new Vector3(((i * 150) - 150 ) + (Screen.width/2), clone.transform.position.y);
            
            desc = card.transform.Find("Description").gameObject;
            desc.GetComponent<TextMeshProUGUI>().color = new Color(1,1,1,0);
            cardname = card.transform.Find("CardName").gameObject;
            cardname.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
            Player.GetComponent<Move>().disableInput = true;
            result = UnityEngine.Random.Range(0, 4).ToString();

            //make sure stuff is not duped.
            if(clone.GetComponent<UpgradeCardBehavior>() != null)
            {
                clone.GetComponent<UpgradeCardBehavior>().upgradeID = numbers[i];
                Debug.Log("result " + i + " assigned ");
            }
            
        }
    }
    public void SpawnSpecialUpgrades()
    {
        GameObject clone;
        //creates 3 UpgradeCard objects, positioning them from left to right
        // in this function, add results and conditonals for new rolls not == results

        numbers.Clear();
        //generate new numbers :D
        if (pickeUpgrades.Count < 3)
        {
            for (int i = 0; i < count; i++)
            {

                int temp = Random.Range(0, 6);
                while (numbers.Contains(temp) || pickeUpgrades.Contains(temp))
                {
                    temp = Random.Range(0, 6);
                }

                numbers.Add(temp);

            }
        }
        else { Debug.Log("player has too many special upgrades, unable to apply more"); }
        

        string result;
        for (int i = 0; i < 3; i++)
        {
            //set the upgradeID of each card to numbers[i]
            GameObject desc;
            GameObject cardname;
            clone = Instantiate(card, transform);
            clone.SetActive(true);
            clone.transform.SetParent(GameObject.Find("Canvas").gameObject.transform, false);
            clone.transform.position = new Vector3(((i * 150) - 150) + (Screen.width / 2), clone.transform.position.y);
            clone.GetComponent<Image>().sprite = altCard;
            
            //change the sprite of the upgrade card to the enhanced one.
            desc = card.transform.Find("Description").gameObject;
            desc.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
            cardname = card.transform.Find("CardName").gameObject;
            cardname.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
            Player.GetComponent<Move>().disableInput = true;
            

            //make sure stuff is not duped.
            if (clone.GetComponent<UpgradeCardBehavior>() != null)
            {
                clone.GetComponent<UpgradeCardBehavior>().upgradeID = numbers[i];
                Debug.Log("result " + i + " assigned ");
                clone.GetComponent<UpgradeCardBehavior>().isSpecial = true;
            }

        }
    }
    public void KillUpgrades()
    {
        //GameObject child;
        for (int i = 0; i < GameObject.Find("Canvas").gameObject.transform.childCount; i++)
        {
            GameObject child = GameObject.Find("Canvas").gameObject.transform.GetChild(i).gameObject;
            if (child.name == card.name + "(Clone)")
            {
                Destroy(child);
            }
            
        }
        Player.GetComponent<Move>().disableInput = false;
        //loop for each child Card and kill them
        //Destroy(card);

    }
}
