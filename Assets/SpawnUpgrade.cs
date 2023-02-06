using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.PlayerLoop;



    public class SpawnUpgrade : MonoBehaviour
    {
        public GameObject spawned;
    public GameObject biomeObject;
    
    public GameObject ringGroup;
    public List<GameObject> ringPieces ;

    PlaceRingComponent childscript;
    //creates the game object when the player touches it.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("guh");
            GameObject upgrades;
            upgrades = Instantiate(spawned);
            upgrades.SetActive(true);
            upgrades.transform.position = this.transform.position;
            //update gameobject array
            biomeObject.GetComponent<SetBiomeLocations>().UpdateBiomeLocation();
            //for (int i = 0; i < ringGroup.transform.childCount; i++) { 
            foreach (Transform child in ringGroup.transform)
            {
                childscript = child.GetComponent<PlaceRingComponent>();
                child.GetComponent<PlaceRingComponent>().PlaceMe(
                0.0f,
                37.0f,
                0.0f,
                childscript.SpawnDistance,
                (childscript.SpawnAngle + Random.Range(childscript.SpawnAngleOffset * -1, childscript.SpawnAngleOffset)) * (Mathf.PI / 180));
                Debug.Log("child" + child.gameObject.name + " set to " + childscript.SpawnAngle + " with " + childscript.SpawnAngleOffset + " offset");
            }
            //refresh map here

        }
    }
}

