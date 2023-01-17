using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingOtherWay : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public float age = 0;
    public float initAngle = 0.0f;
    public int hitCount = 0;

    Quaternion pos;
    // Start is called before the first frame update
    void Start()
    {
        //initAngle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (age > 0.25)
        {
            Debug.Log(target.transform.localRotation.z);
            Destroy(target);
            player.GetComponent<Move>().isSwingin = false;
        }
        age += 1 * Time.deltaTime;
        //body = target.GetComponent<Rigidbody2D>();
        pos = target.transform.rotation;
        target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0,Mathf.Max(initAngle + (0 - (720 * age )), initAngle - 90) + target.transform.rotation.z));
    }
}