using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //repeatedly calls while colliding
    }


}
