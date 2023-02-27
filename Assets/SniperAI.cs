using PlasticGui.SwitcherWindow.Workspaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityEngine.Localization
{
    public class SniperAI : MonoBehaviour
    {
        public Transform target;
        NavMeshAgent agent;

        GameObject player;

        public float pathAge;
        public float abilityCD;
        public bool usingAbility;

        public float destX;
        public float destY;

        public bool canShoot;
        public float shotCharge;

        public GameObject projectile;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player");
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            abilityCD += (Random.Range(0, 5)) * 0.1f;
            destX = agent.destination.x;
            destY = agent.destination.y;
            SetTarget();
        }
       
        void SetTarget()
        {
            var angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * 180/ Mathf.PI;
            //get angle between player and enemy +- a bit
            //set new path destination to somewhere within the cos sin arc of the player's angle
            angle -= 180;
            angle += Random.Range(-5,5) * 180/Mathf.PI;
            var xTar = target.position.x + 11f *Mathf.Cos(angle);
            var yTar = target.position.y +  4f * Mathf.Sin(angle);
            agent.SetDestination(new Vector3(xTar,yTar));
            destX = agent.destination.x;
            destY = agent.destination.y;
            pathAge = 0;
        }
        void LaunchProjectile()
        {
            GameObject woop;
            //used for shooting at the player
            Debug.Log("Shoot!");
            woop = Instantiate(projectile);
            woop.SetActive(true);
            woop.transform.position = transform.position;
            var rotation = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * 180 / Mathf.PI;
            woop.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation - 90));
            //after the unit shoots, update position.
            SetTarget();
        }
        // Update is called once per frame
        void Update()
        {
            //the sniper wants to move close to the player, but not too close.
            if (agent.destination.x < gameObject.transform.position.x)
            {
                //look left
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                //look right
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            pathAge += 1.0f * Time.deltaTime;
            //updates path when it reaches its target.
            if (agent.pathEndPosition.x == transform.position.x && abilityCD <= 0)
            {
                
                //shoots at the player here.
                    Debug.Log("reached end of path");
                    abilityCD = 1.5f;
                //this function also creates the tracer line for the shot.
                canShoot = true;
            }

            if (abilityCD > 0)
            {
                abilityCD -= 1.0f * Time.deltaTime;
            }
            var distance = Vector2.Distance(transform.position, player.transform.position);
            
            if (abilityCD > 0f && canShoot == true)
            {
                //charges up projectile.
                shotCharge += 1.0f * Time.deltaTime;
                if (shotCharge >= 1.0)
                {
                    LaunchProjectile();
                    shotCharge = 0;
                    canShoot = false;
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                //hurt the player
                //collision.gameObject.GetComponent<Move>().TakeDamage(1, 0.25f);
                //Debug.Log("damaged player! health is now " + collision.gameObject.GetComponent<Move>().Health);
            }
        }
    }
}
