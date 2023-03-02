using Codice.Client.BaseCommands;
using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

public class Move : MonoBehaviour
{
    //player movespeed
    public float speed = 10.0f;
    //
    public string EquippedWeapon = "Sword";
    //is the player charging their sword throw? used for stopping movement
    private bool isChargingUp = false;
    //how fast the sword is readied
    public float chargeRate = 6.0f;
    //the sword's curent charge
    public float charge = 0.0f;
    //number required before the launch event is fired
    public float chargeMax = 0.5f;
    Rigidbody2D plr;
    public GameObject Projectile;
    public GameObject Player;
    public GameObject Swing;
    public GameObject OtherSwing;

    // this is written to when the player picks up a new shiny upgrade that actually changes how the weapon is played.
    //maybe should be a list
    public int[] displayedUpgrades = new int[3] { 0, 0, 0};
    private int[] possibleUpgrades = new int[6] { 1, 2, 3, 4, 5, 6 };
    //public string[] possibleUpgrades = new string[6] { "Homing", "Exploding", "Special", "Kills", "Superpower", "Wowzers" };
    public int[] pickedUpgrades = new int[3] { 0, 0, 0 };

    Animator anim;
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
    //disable the input of the player when true;
    public bool disableInput = false;
    //stats
    public int Health;
    public int MaxHealth;
    public int Damage;

    public float IFrames;
    public int Area;
    public int Speed;
    public int Range;
    public int Duration;
    public int Evasion;
    public int Siphon;

    public bool freeplay;

    public float empowerDuration;

    private void Start()
    {
        plr = GetComponent<Rigidbody2D>();
        Debug.Log(displayedUpgrades.Length);
        RollSpecialUpgrades();
    }
   
    //for rolling special upgrades, make sure none of the rolled upgrades equal chosen upgrades
    void RollSpecialUpgrades()
    {
        
    }
    //uses secondary attack
    void Launch(float a, float b, float power)
    {
        Vector3 pos = transform.position;
        float rot = Mathf.Atan2(a - pos.x,b - pos.y) * Mathf.Rad2Deg;
        Player.GetComponent<WeaponOverseer>().SpecialAttack(EquippedWeapon, rot, power);
    }

    //creates the swing projectile
    void Attack(float a, float b, int hitNumber)
    {
        Vector3 pos = transform.position;
        float rot = Mathf.Atan2(a - pos.x, b - pos.y) * Mathf.Rad2Deg;
        Player.GetComponent<WeaponOverseer>().BasicAttack(EquippedWeapon, rot);
    }
    //backswing attack
    void Attack1(float a, float b, int hitNumber)
    {
        Vector3 pos = transform.position;
        float rot = Mathf.Atan2(a - pos.x, b - pos.y) * Mathf.Rad2Deg;
        Player.GetComponent<WeaponOverseer>().BasicBackSwingAttack(EquippedWeapon, rot);
    }

    public void AddUpgrade(string name, int level)
    {
        //adds Level to the value of the string variable
        level += 1;
        Debug.Log("upgrading stat " + name + " with level " + level);
        if (name == "Health") { MaxHealth += level; Health = MaxHealth; }
        if (name == "Haste") { attackSpeed += level; }
        if (name == "Attack") { Damage += level; }
        if (name == "Speed") { Speed += level; }
        if (name == "Area") { Area += level; }
        //new
        if (name == "Evasion") { Evasion += level; }
        if (name == "Siphon") { Siphon += level; }
        if (name == "Duration") { Duration += level; }
        //"Attack", "Speed", "Haste", "Health", "Area"
    }
    public void TakeDamage(int value, float iframes)
    {
        if (value > 0 && IFrames <= 0) 
        {
            Health -= value;
            IFrames += iframes + (0.5f * Evasion);
        }
        if(value < 0)
        {
            Health -= value;
        }
        
        if(Health > MaxHealth) { Health = MaxHealth; }
        if (Health < 0) { Health = 0; }
        if(Health == 0) { updater.LoseGame(); }
    }
  
    //Update method
    void Update()
    {
        //dubug re-run rolls
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RollSpecialUpgrades();
        }
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
        if(IFrames > 0)
        {
            IFrames -= 1 * Time.deltaTime;
        }
        if(empowerDuration > 0)
        {
            empowerDuration -= 1 * Time.deltaTime;
        }
        if (disableInput == false){ 
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
                //hasSword = false;
                charge = 0;
            }
        }
        //Lets the player weapon actions
        if (hasSword == true && attackDelay <= 0)
        {
            //basic attack
            if (Input.GetKey(KeyCode.Mouse0) && isChargingUp == false && isSwingin == false)
            {


                if (hitCount < hitMax)
                {
                    Attack(worldPosition.x, worldPosition.y, 0);
                        if (updater.pickeUpgrades.Contains(1))
                        {
                            Attack1(worldPosition.x, worldPosition.y, 0);
                        }
                        isSwingin = true;
                    attackDelay = (baseAttackTime * (1.0f - (attackSpeed/10.0f))) * 0.50f;
                    hitCount += 1;
                }
                else
                {
                    Attack1(worldPosition.x, worldPosition.y, 1);
                        if (updater.pickeUpgrades.Contains(1))
                        {
                            Attack(worldPosition.x, worldPosition.y, 0);
                        }
                    isSwingin = true;
                    attackDelay = (baseAttackTime * (1.0f - (attackSpeed/10.0f))) * 1.0f;
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
                    if (!updater.pickeUpgrades.Contains(2)) { charge += chargeRate * Time.deltaTime; }
                    else { charge += (chargeRate*2f) * Time.deltaTime; }
                //throw overcharged sword
                if (charge > chargeMax + 3)
                {
                    Launch(worldPosition.x, worldPosition.y, charge);
                    //hasSword = false;
                    charge = 0;
                }
            }
        }
    }
    }
    private void FixedUpdate()
    {
        //stops movement when swinging or charging throw
        if (isChargingUp == true || disableInput == true)
        {
            horizontal = 0;
            vertical = 0;
        }
        //reduces movement speed by 30% when moving diagonally
        if (isChargingUp == false)
        {
            if (horizontal != 0 && vertical != 0) 
            {
                horizontal *= 0.7f;
                vertical *= 0.7f;
            }
            //halves movespeed when swinging
            if (isSwingin == true)
            {
                horizontal *= 0.5f;
                vertical *= 0.5f;
            }
        }
        anim = this.gameObject.GetComponent<Animator>();
        //update the player's current animation state
        if (vertical < 0 && horizontal == 0) { anim.Play("PlayerFront", 0);}
        if (vertical > 0 && horizontal == 0) {anim.Play("PlayerBack", 0);}
        if (horizontal > 0) { anim.Play("PlayerRight", 0); }
        if (horizontal < 0) { anim.Play("PlayerLeft", 0); }
        if(horizontal == 0 && vertical == 0) {anim.StopPlayback();}
        //update position
        plr.velocity = new Vector2(horizontal * (float)(speed + Speed), vertical * (float)(speed+Speed));
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
        if(collision.name == "SpecialSword(Clone)" && collision.GetComponent<ProjectileBehavior>() != null)
        {
            if (collision.GetComponent<ProjectileBehavior>().hitWall == true)
            {
                Debug.Log("Player picks up sword");
                hasSword = true;
                Destroy(collision.gameObject);
                updater.UpdateSword();
            }
        }
    }
}