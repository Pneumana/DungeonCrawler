using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

public class SwingOtherWay : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public float age = 0;
    public float initAngle = 0.0f;
    public int hitCount = 0;
    public bool launch = false;
    public GameObject launchProjectile = null;
    private bool hasShot;
    Quaternion pos;

    UIUpdater ui;
    // Start is called before the first frame update
    void Start()
    {
        //initAngle = transform.rotation.z;
        ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
    }
    public void SetStartingAngle(float rotation)
    {
        initAngle = -rotation + 45;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy " + collision.gameObject.name + " for " + (int)((player.GetComponent<Move>().Damage + 3)) + " damage");
        }
        if (collision.gameObject.name == "Circle(Clone)")
        {
            Debug.Log("parry");
            if (ui.pickeUpgrades.Contains(2))
            {

                Destroy(collision.gameObject);
                GameObject seeker;
                seeker = Instantiate(GameObject.Find("ProjectileStorage").transform.Find("FromUpgrades").transform.Find("Parried").gameObject);
                seeker.SetActive(true);
                Debug.Log("create parry projectile " + seeker.name);
                seeker.transform.position = player.transform.position;
                seeker.GetComponent<GenericProjectile>().speed = 30;
                seeker.GetComponent<GenericProjectile>().isEnemy = false;
                
                GetClosestEnemy(seeker.gameObject);
            }
        }
    }
   
    void GetClosestEnemy(GameObject targetObj)
    {
        float current = 99999f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //float distance = Mathf.Abs((enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y));
            float distance = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
            if (enemy.GetComponent<EnemyBody>() != null)
            {
                //target = enemy.transform;
            }
            if (distance < current)
            {
                Debug.Log("new closest " + enemy.name + " with distance of " + distance);
                current = distance;
                var rot = Mathf.Atan2(targetObj.transform.position.y - enemy.transform.position.y, targetObj.transform.position.x - enemy.transform.position.x) * 180 / Mathf.PI;
                targetObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rot + 90));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (age > 0.25)
        {
            //Debug.Log(target.transform.localRotation.z);
            Destroy(target);
            player.GetComponent<Move>().isSwingin = false;
        }
        if (age > 0.0625 && launch == true && hasShot == false)
        {
            GameObject projectilie;
            projectilie = Instantiate(launchProjectile);
            projectilie.SetActive(true);
            projectilie.transform.position = player.transform.position;
            projectilie.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Max(initAngle + (0 - (720 * age)), initAngle - 90) + target.transform.rotation.z));
            // if the attack can throw a projectile, throw it here.
            Debug.Log("shoot!");
            hasShot = true;
        }
        age += (1 + (0.1f * player.GetComponent<Move>().attackSpeed)) * Time.deltaTime;
        //body = target.GetComponent<Rigidbody2D>();
        target.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        pos = target.transform.rotation;
        target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0,Mathf.Max(initAngle + (0 - (720 * age )), initAngle - 90) + target.transform.rotation.z));
    }
}
