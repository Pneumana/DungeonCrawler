
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradeCardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject card;
    private bool mouseOver = false;
    public int tier = 0;
    public int upgradeID = 0;
    public int cardID = 0;
    public string upgrade = "Blade";

    public float scaleMultiplier;

    public bool isSpecial;

    public Sprite[] sprites;
    public Sprite[] specialSprites;

    string[] listUpgradeNames = new string[] {"Attack", "Speed", "Haste", "Health", "Area", "Evasion", "Siphon", "Duration"};
    string[] listGoodUpgrades = new string[] { "Ghosts", "Scissor", "Piercing", "Turnabout", "Cilla", "DeepWounds"};

    string[] displayDesc = new string[] {
        "Increases damage by 1",
        "Increases movement speed by 10%",
        "Increases attack speed by 10%",
        "Increases max health by 1. Heal to full when this upgrade is picked.",
        "Increases size of attacks by 10%",
        "Increases the duration of invulnerability by 0.5 seconds.",
        "Reduces the speed that health drains at. You can heal when killing enemies while low on health.",
        "Increases the duration of lingering attacks and projectiles by 0.25 seconds"
    };

    string[] displaySpecialDesc = new string[] { 
        "Kills create seeking ghosts that damage the nearest enemy",
        "Swings in both directions at the same time",
        "Reflect projectiles with your basic attack",
        "Killing enemies empowers you. While empowered, nearby enemies take damage every second",
        "Each time an enemy is damaged, they take additional damage.",
        "Each attack on an enemy increases the damage they take by 2."};
    string[] displaySpecialNames = new string[] { 
        "Death Seekers",
        "Scissor Style",
        "Parrying Blade",
        "Holy Retribution",
        "Smite",
        "Deep Wounds"};
    public GameObject desc;
    public GameObject cardname;
    public GameObject image;
    public GameObject player;

    public UIUpdater ui;

    float scaleUp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
        tier = 0;
        Refresh();
    }

    public void Refresh()
    {
        if (isSpecial == true)
        {
            desc.GetComponent<TextMeshProUGUI>().text = displaySpecialDesc[upgradeID];
            cardname.GetComponent<TextMeshProUGUI>().text = displaySpecialNames[upgradeID];
            image.GetComponent<UnityEngine.UI.Image>().sprite = specialSprites[upgradeID];
        }
        if (!isSpecial)
        {
            desc.GetComponent<TextMeshProUGUI>().text = displayDesc[upgradeID];
            cardname.GetComponent<TextMeshProUGUI>().text = listUpgradeNames[upgradeID];
            image.GetComponent<UnityEngine.UI.Image>().sprite = sprites[upgradeID];
        }
        /*var localizedName = cardname.GetComponent<LocalizeStringEvent>();
        localizedName.SetTable("UIStrings");
        localizedName.SetEntry(cardname.GetComponent<TextMeshProUGUI>().text);
        var localizedDesc = desc.GetComponent<LocalizeStringEvent>();
        localizedDesc.SetTable("UIStrings");
        localizedDesc.SetEntry(desc.GetComponent<TextMeshProUGUI>().text);*/
        //cardname.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = cardname.GetComponent<TextMeshProUGUI>().text;
        //desc.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = desc.GetComponent<TextMeshProUGUI>().text;
        scaleUp = 0;
        transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 2) * (1 + scaleUp), 1.0f);
    }

    void Update()
    {
        if (mouseOver == true)
        {
            if (scaleUp < 0.2f)
            {
                transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 2) * (1 + scaleUp), 1.0f);
                scaleUp += 1f * Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if(isSpecial == true)
                {
                    ui.pickeUpgrades.Add(upgradeID);
                }
                if(isSpecial == false) { player.GetComponent<Move>().AddUpgrade(listUpgradeNames[upgradeID], tier); }
                Debug.Log("Selected upgrade " + cardname + " with tier " + tier);
                
                ui.KillUpgrades();
            }
        }
        if (mouseOver == false)
        {
            if (scaleUp > 0f)
            {
                transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 2) *(1 + scaleUp), 1.0f);
                scaleUp -= 1f * Time.deltaTime;
            }
        }
        
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        Debug.Log("Mouse exit");
    }
    //when the player clicks on an upgrade, use sin to increase the selected boon slightly, and shrink the unselected
}
