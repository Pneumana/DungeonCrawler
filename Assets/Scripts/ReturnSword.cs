using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class ReturnSword : MonoBehaviour
{
    GameObject plr;
    Rigidbody2D body;
    CapsuleCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        plr = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.transform.GetComponent<EnemyBody>() != null)
        {
            collision.gameObject.GetComponent<EnemyBody>().TakeDamage(plr.GetComponent<Move>().Damage * 2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
       
        //bring the sword back to the player
        //this functionality is kinda cool.
        float rot = Mathf.Atan2(plr.transform.position.x - pos.x, plr.transform.position.y - pos.y) * Mathf.Rad2Deg;
        body.rotation = -rot;
        body.transform.position += transform.up * (20.0f * Time.deltaTime);
    }
}
