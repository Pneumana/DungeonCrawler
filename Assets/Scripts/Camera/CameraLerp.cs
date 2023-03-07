using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class CameraLerp : MonoBehaviour
    {
        Vector2 target;
        Vector2 current;
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            current = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            target = new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y);
            var newPosition = Vector2.Lerp(current, target, Time.deltaTime * 10.0f);
            gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, -10f);
        }
    }
}
