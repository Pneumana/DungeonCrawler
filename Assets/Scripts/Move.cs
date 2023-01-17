using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    //player movespeed
    public float speed = 10.0f;
    //is the player charging their sword throw? used for stopping movement
    private bool isChargingUp = false;
    //how fast the sword is readied
    public float chargeRate = 2.0f;
    //the sword's curent charge
    public float charge = 0.0f;
    //number required before the launch event is fired
    public float chargeMax = 1.25f;
    Rigidbody2D plr;
    public GameObject Projectile;
    public GameObject Player;
    public GameObject Swing;
    public GameObject OtherSwing;
    //add specific weapon classes that are invoked using equippedWeapon.Primary or equippedWeapon.Secondary
    //attack chains are in the weapon's class
    public GameObject equippedWeapon;
    //used for UI updates
    public UIUpdater updater;
    //gets the mouse position
    public float mouseX = 0.0f;
    public float mouseY = 0.0f;
    private Vector2 worldPosition = Vector2.zero;

    //prevents using attacks
    private float attackDelay = 0.0f;
    //works as a percentage. reduces the attackDelay * (1 - attackSpeed)
    public float attackSpeed = 0.0f;
    //used for left-right swinging. if hitcount is 1 and equipped weapon is Sword, increase attackDelay.
    public int hitCount;
    public int hitMax = 1;
    //base cooldown between attacks in seconds
    public float baseAttackTime = 0.50f;

    float horizontal;
    float vertical;
    
    //is the player holding their sword?
    public bool hasSword = true;
    //is the player using their weapon?
    public bool isSwingin = false;
    private void Start()
    {
        plr = GetComponent<Rigidbody2D>();
    }
    //function used to throw the sword. A is MouseX and B is MouseY with the power being the force the projectile is launched with.
    void Launch(float a, float b, float power)
    {
        GameObject clone;
        Rigidbody2D hitbox;
        Vector3 pos = transform.position;
        clone = Instantiate(Projectile, transform);
        hitbox = clone.GetComponent<Rigidbody2D>();
        clone.SetActive(true);
        clone.transform.position = pos;
        float rot = Mathf.Atan2(a - pos.x,b - pos.y) * Mathf.Rad2Deg;
        hitbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rot));
        ProjectileBehavior pb = clone.GetComponent<ProjectileBehavior>();
        //min(max(minpower, (power*2) * 40.0f), maxpower)
        pb.speed = Mathf.Min(Mathf.Max(15.0f,(power*2.0f) * 40.0f), 120.0f);
        clone.transform.parent = null;
        updater.UpdateSword();
        Debug.Log("created clone @ " +transform.position + "with rotation " +rot + " with power " +power);
    }

    //creates the swing projectile
    void Attack(float a, float b, int hitNumber)
    {
        GameObject clone;
        Rigidbody2D hitbox;
        Vector3 pos = transform.position;
        clone = Instantiate(Swing, transform);
        hitbox = clone.GetComponent<Rigidbody2D>();
        clone.SetActive(true);
        clone.transform.position = pos;
        float rot = Mathf.Atan2(a - pos.x, b - pos.y) * Mathf.Rad2Deg;
        clone.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -rot + ((90 * hitNumber)- 45)));
        clone.GetComponent<SwingAttack>().initAngle = -rot + ((90 * hitNumber)- 45);
        clone.GetComponent<SwingAttack>().hitCount = hitNumber;
    }
    void Attack1(float a, float b, int hitNumber)
    {
        GameObject clone;
        Rigidbody2D hitbox;
        Vector3 pos = transform.position;
        clone = Instantiate(OtherSwing, transform);
        hitbox = clone.GetComponent<Rigidbody2D>();
        clone.SetActive(true);
        clone.transform.position = pos;
        float rot = Mathf.Atan2(a - pos.x, b - pos.y) * Mathf.Rad2Deg;
        clone.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -rot + ((90 * hitNumber) - 45)));
        clone.GetComponent<SwingOtherWay>().initAngle = -rot + ((90 * hitNumber) - 45);
        clone.GetComponent<SwingOtherWay>().hitCount = hitNumber;
    }
    //Update method
    void Update()
    {
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Aim
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //cooldown attack
        if (attackDelay >= 0)
        {
            attackDelay -= 1 * Time.deltaTime;
        }

        //restricts actions when the player starts charging up the sword throw.
        if (isChargingUp == true)
        {
            //Cancles the input
            if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyUp(KeyCode.Mouse1) && charge < chargeMax)
            {
                isChargingUp = false;
                charge = 0;
                //this should make the player stop charging the throw.
            }
            //Throws the sword
            if (Input.GetKeyUp(KeyCode.Mouse1) && charge >= chargeMax)
            {
                isChargingUp = false;
                Launch(worldPosition.x, worldPosition.y, charge);
                hasSword = false;
                charge = 0;
            }
        }
        //Lets the player weapon actions
        if (hasSword == true && attackDelay <= 0)
        {
            //basic attack
            if (Input.GetKey(KeyCode.Mouse0) && isChargingUp == false && isSwingin == false )
            {
               
                
                if(hitCount < hitMax)
                {
                    Attack(worldPosition.x, worldPosition.y, 0);
                    isSwingin = true;
                    attackDelay = (baseAttackTime * (1.0f - attackSpeed)) * 1.0f;
                    hitCount += 1;
                }
                else
                {
                    Attack1(worldPosition.x, worldPosition.y, 1);
                    isSwingin = true;
                    attackDelay = (baseAttackTime * (1.0f - attackSpeed)) * 2.0f;
                    hitCount = 0;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isChargingUp = true;
            }
            //secondary attack
            if (Input.GetKey(KeyCode.Mouse1) && isChargingUp == true && isSwingin == false)
            {
                //charge sword throw
                charge += chargeRate * Time.deltaTime;
                //throw overcharged sword
                if (charge > chargeMax + 3)
                {
                Launch(worldPosition.x, worldPosition.y, charge);
                    hasSword = false;
                    charge = 0;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        //stops movement when swinging or charging throw
        if (isChargingUp == true || isSwingin == true)
        {
            horizontal = 0;
            vertical = 0;
        }
        //reduces movement speed by 30% when moving diagonally
        if (isChargingUp == false && isSwingin == false)
        {
            if (horizontal != 0 && vertical != 0) 
            {
                horizontal *= 0.7f;
                vertical *= 0.7f;
            }
            
        }
        //update position
        plr.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit trigger " + collision.name);
        //Pick up the sword
        if (collision.name == "ReturnSword(Clone)")
        {
            hasSword = true;
            Destroy(collision.gameObject);
            updater.UpdateSword();
        }
    }
}