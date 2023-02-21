using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityEngine.Localization
{
    public class SlimeAI : MonoBehaviour
    {
        public Transform target;
        NavMeshAgent agent;

        GameObject player;

        public float pathAge;
        public float abilityCD;
        public bool usingAbility;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player");
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            abilityCD += (Random.Range(0, 5)) * 0.1f;
        }
        void SetTarget()
        {
            agent.SetDestination(target.position);
        }
        // Update is called once per frame
        void Update()
        {
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
            //updates path every 0.25 seconds
            if (pathAge >= 0.25f)
            {
                SetTarget();
                pathAge = 0;
            }
            if(abilityCD >0)
            {
                abilityCD -= 1.0f * Time.deltaTime;
            }
            var distance = Vector2.Distance(transform.position, player.transform.position);
            if (abilityCD <= 0)
            {
                //use ability lunge
                abilityCD = 1.5f;
                agent.speed = 20;
            }
            if(abilityCD < 1)
            {
                agent.speed = 1;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name == "Player")
            {
                //hurt the player
                collision.gameObject.GetComponent<Move>().Health -= 1;
                Debug.Log("damaged player! health is now " + collision.gameObject.GetComponent<Move>().Health);
            }
        }
    }
}
