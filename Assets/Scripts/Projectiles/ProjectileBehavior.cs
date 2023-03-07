using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

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
    UIUpdater ui;
    //private GameObject light;
    public int rotation = 0;
    public bool real = false;
    public bool hitWall = false;
    private Vector2 worldPosition = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        //set angle to look at cursor
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        //trail = GetComponent<TrailRenderer>();
        body.AddForce(transform.up * speed, ForceMode2D.Impulse);
        ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
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
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Vector3 pos = transform.position;
            plr = GameObject.Find("Player");
            hitWall = true;
            
            
        }
        if (collision.gameObject.tag == "Enemy" && hitWall == false)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Vector3 pos = transform.position;
            plr = GameObject.Find("Player");
            hitWall = true;
        }

    }
    void RecallSword()
    {
        plr = GameObject.Find("Player");
        rtrnSword = Instantiate(sword);
        rtrnSword.transform.position = transform.position;
        rtrnSword.SetActive(true);
        rtrnSword.transform.localScale = new Vector3(1.0f + (float)(0.1f * plr.GetComponent<Move>().Area), 1.0f + (float)(0.1f * plr.GetComponent<Move>().Area));
        Destroy(projectile);
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
            if (lifeSpan >= maxLife && hitWall==false)
            {
                hitWall = true;
            }
            //projectile.transform.position += transform.up * (speed*Time.deltaTime);
            if (hitWall == true)
            {
                this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                gameObject.layer = 7;
                floorAge += 1 * Time.deltaTime;
                body.rotation += (Mathf.Max(1000.0f - floorAge, 0)) * Time.deltaTime;
                //light = projectile.transform.Find("Glow").gameObject;
                //light.GetComponent<Light>().spotAngle = (floorAge / 1500) * 30;
            }
            //fires once
            if (floorAge >= 5.0f)
            {
                RecallSword();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RecallSword();
            }

        }
    }
}

    

