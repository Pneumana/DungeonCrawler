using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraTrigger : MonoBehaviour
{
    public string direction;
    public MoveCamera cameraScript;
    public bool inactive;
    //private GameObject camera;
    void Awake() 
    {
        //camera = GameObject.Find ("MainCamera");
    } 
    //MoveCamera cam = new MoveCamera();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && inactive == false)
        {
            cameraScript.ChangeTargetPos(direction);
            Debug.Log("Player Touched " + direction);
        }
    }
}
