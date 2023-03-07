using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityEngine.Localization
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform target;
        NavMeshAgent agent;

        public int NumberToSpawn;

        public bool allowed;

        GameObject player;
        public GameObject[] spawnableEnemies;

        UIUpdater ui;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("Player");
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
        }
        void SetTarget()
        {
            var angle = 0f;
            angle += Random.Range(-180, 180) * 180 / Mathf.PI;
            var xTar = transform.position.x + 8f * Mathf.Cos(angle);
            var yTar = transform.position.y + 8f * Mathf.Sin(angle);
            agent.SetDestination(new Vector3(xTar, yTar));
            //spawn a new enemy here
        }

        public void ReEnableSpawning()
        {
            allowed = true;
            NumberToSpawn = Random.Range(2, 6) + (int)ui.freePlayWaves;
        }
        public void DisableSpawning()
        {
            allowed = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (agent.pathEndPosition.x == transform.position.x)
            {
                
                if (NumberToSpawn > 0 && allowed) 
                {
                    NumberToSpawn--;
                    SetTarget();
                    GameObject newenemy;
                    newenemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length)]);
                    newenemy.transform.position = this.transform.position;
                    newenemy.SetActive(true);
                    Debug.Log("spawned new " + newenemy.name);
                    ui.UpdateEnemyNumber();
                }
                if(NumberToSpawn == 0) { allowed = false; }
            }
            //Allows the game to spawn more enemies needs to wait until the player pickes an upgrade.
            if(NumberToSpawn == 0 && ui.enemyTotal == 0 && allowed)
            {
                NumberToSpawn = Random.Range(2, 6);
            }
        }
    }
}
