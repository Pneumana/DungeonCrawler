using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class ProjectileBehavior : MonoBehaviour
{
    Rigidbody2D body;
    CapsuleCollider2D hitbox;
    GameObject plr;
    GameObject rtrnSword;
    //TrailRenderer trail;
    float floorAge;
    public float speed = 0.0f;
    public float lifeSpan = 0.0f;
    public float maxLife = 1.0f;
    public GameObject projectile;
    public GameObject spinner;
    public GameObject sword;
    public int rotation = 0;
    public bool real = false;
    bool hitWall = false;
    private Vector2 worldPosition = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        //set angle to look at cursor
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        //trail = GetComponent<TrailRenderer>();
        body.AddForce(transform.up * speed, ForceMode2D.Impulse);
        //Set the speed of the GameObject
        //speed = 40.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit " + collision.gameObject.name);
        //needs to hit an object tagged as Wall to stop
        if (collision.gameObject.tag == "Wall")
        {
            //hitbox.isTrigger = true;
            Vector3 pos = transform.position;
            plr = GameObject.Find("Player");
            hitWall = true;
            //this bit here makes the sword look at the player before spinning
            //float rot = Mathf.Atan2(plr.transform.position.x - pos.x, plr.transform.position.y - pos.y) * Mathf.Rad2Deg;
            //body.rotation = -rot;
            //body.AddForce(transform.up * (10.0f), ForceMode2D.Impulse);
            //body.drag = 1.0f;
            //body.angularDrag = 1.0f;
            //body.angularVelocity = 100.0f;

            //trail.emitting = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.activeSelf == true)
        {
            //Vector3 pos = projectile.transform.position;
            //float rot = projectile.transform.rotation.z;
            lifeSpan += 1.0f * Time.deltaTime;
            //hitbox.velocity = transform.up * speed;
            //pos = projectile.transform.forward;
            if (lifeSpan >= maxLife)
            {
                //Destroy(projectile);
            }
            //projectile.transform.position += transform.up * (speed*Time.deltaTime);
            if (hitWall == true)
            {
                floorAge += 1;
                body.rotation += (Mathf.Max(1000.0f - floorAge, 0)) * Time.deltaTime;
            }
            //fires once
            if (floorAge >= 1500.0f)
            {
                rtrnSword = Instantiate(sword);
                rtrnSword.transform.position = transform.position;
                rtrnSword.SetActive(true);
                Destroy(projectile);
                /*Vector3 pos = transform.position;
                plr = GameObject.Find("Player");
                //bring the sword back to the player
                //this functionality is kinda cool.
                float rot = Mathf.Atan2(plr.transform.position.x - pos.x, plr.transform.position.y - pos.y) * Mathf.Rad2Deg;
                body.rotation = -rot;
                body.AddForce(transform.up * (10.0f * Time.deltaTime), ForceMode2D.Impulse);*/
            }
            

        }
    }
}

    

