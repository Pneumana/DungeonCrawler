using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class ExpandMap : MonoBehaviour
    {
    public int MapRadius = 20;
        // Start is called before the first frame update
        void Start()
        {
        MapRadius = 20;
        GameObject.Find("AllRingParts/LushBarrier").GetComponent<PlaceRingComponent>().SpawnDistance = MapRadius;
        GameObject.Find("AllRingParts/RuinsBarrier").GetComponent<PlaceRingComponent>().SpawnDistance = MapRadius;
        GameObject.Find("AllRingParts/BarrenBarrier").GetComponent<PlaceRingComponent>().SpawnDistance = MapRadius;
    }

        // Update is called once per frame
        private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.name == "Player")
        { 
            //set map to x + 20 then kill

        }
        }
    }
