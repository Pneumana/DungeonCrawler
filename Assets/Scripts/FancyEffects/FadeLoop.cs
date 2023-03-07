using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class FadeLoop : MonoBehaviour
    {
        float loop;
        bool flop;

        bool kickStart;

        Move player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Move>();
        }

        // Update is called once per frame
        void Update()
        {
            if(player.empowerDuration > 0 && !kickStart) { kickStart = true; loop = 1; }
            if (player.empowerDuration <= 0 && kickStart) { kickStart = false; }
            if (loop < 1 && flop) { loop += 1f * Time.deltaTime; }
            if (loop >= 1 && flop) { flop = false; }
            if (loop > 0 && !flop) { loop -= 1f * Time.deltaTime; }
            if (loop <= 0 && !flop && player.empowerDuration > 0) { flop = true; }
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,loop * 0.5f);
        }
    }
}
