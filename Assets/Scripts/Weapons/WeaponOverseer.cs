using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class WeaponOverseer : MonoBehaviour
    {
        public GameObject ps;
        public Move playerScript;
        public GameObject player;
        public UIUpdater ui;
        //variableForPrefab = Resources.Load("prefabs/prefab1", GameObject) as GameObject;
        private void Start()
        {
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
        }
        //this script recieves attack commands from the player and issues spawning to projectiles
        public void BasicAttack(string weaponName, float rotation)
        {
            //weaponName and the float are required for spawning.
            GameObject projectile;
            projectile = Instantiate(ps.transform.Find(weaponName).Find("Basic" + weaponName).gameObject);
            //projectile = Instantiate(GameObject.Find("Basic" + weaponName));
            projectile.SetActive(true);
            projectile.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            projectile.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -rotation + ((0) - 45)));
            projectile.transform.localScale = new Vector3(1.0f + (float)(0.1f * playerScript.Area), 1.0f + (float)(0.1f * (playerScript.Area + 1)));
            //somehow add function to swing projectiles to launch 
            projectile.GetComponent<SwingAttack>().SetStartingAngle(rotation);
            if (ui.pickeUpgrades.Contains(2))
            {
                projectile.layer = 12;
            }
        }
        public void BasicBackSwingAttack(string weaponName, float rotation)
        {
            //weaponName and the float are required for spawning.
            GameObject projectile;
            projectile = Instantiate(ps.transform.Find(weaponName).Find("BasicBackswing" + weaponName).gameObject);
            projectile.SetActive(true);
            projectile.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            projectile.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -rotation + ((0) + 45)));
            projectile.transform.localScale = new Vector3(1.0f + (float)(0.1f * playerScript.Area), 1.0f + (float)(0.1f * (playerScript.Area + 1)));
            //somehow add function to swing projectiles to launch 
            projectile.GetComponent<SwingOtherWay>().SetStartingAngle(rotation);
            if (ui.pickeUpgrades.Contains(2))
            {
                projectile.layer = 12;
            }
        }
        public void SpecialAttack(string weaponName, float rotation, float power)
        {
            GameObject projectile;
            projectile = Instantiate(ps.transform.Find(weaponName).Find("Special" + weaponName).gameObject);
            projectile.SetActive(true);
            projectile.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            projectile.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -rotation));
            projectile.transform.localScale = new Vector3(1.0f + (float)(0.1f * playerScript.Area), 1.0f + (float)(0.1f * (playerScript.Area + 1)));
            if (projectile.GetComponent<ProjectileBehavior>() != null)
            {
                ProjectileBehavior pb = projectile.GetComponent<ProjectileBehavior>();
                pb.speed = 40.0f;
            }
            if (ui.pickeUpgrades.Contains(2))
            {
                projectile.layer = 12;
            }
            //power is also charge time
            if (weaponName == "Sword") 
            {
                playerScript.hasSword = false;
                Debug.Log("player looses their weapon");
            }
        }
    }
}
