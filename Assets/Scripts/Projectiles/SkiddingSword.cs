using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiddingSword : MonoBehaviour
{
    public float lifeSpan = 0;
    public float rot = 0.0f;
    public float rotSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpan < 3)
        {
            lifeSpan += 1f * Time.deltaTime;
            rot += rotSpeed;
            rotSpeed = rotSpeed - 0.003f;
            //rotSpeed -= 0.1f * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, transform.position.y, (Mathf.Sin(lifeSpan) * 3.0f) - 0.10f);
            transform.localScale = new Vector3(Mathf.Max((Mathf.Sin(lifeSpan) * 0.1f), 0.0f) + 0.1f, 0.2f, Mathf.Max(((Mathf.Sin(lifeSpan)) * 0.2f) , 0.0f) + 0.2f);
            transform.rotation = Quaternion.Euler(new Vector3(rot, 90, -90));
            //Debug.Log(transform.localScale);
        }
        if (lifeSpan >= 3)
        {
            //lifeSpan = 0;
            //Debug.Log("Sword Landed");
        }
        
    }
}
