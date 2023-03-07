using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject down;
    public GameObject up;
    public GameObject left;
    public GameObject right;

    public float targetX;
    public float targetY;

    bool setup;
    void Awake()
    {
        //set the targetX and targetY to the position of the camera
        if (setup == false){
            targetX = transform.position.x;
            targetY = transform.position.y;
            setup = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Debug.Log(targetX + " , " + targetY);
        //check X and Y posistion to target and update accordingly
        if (transform.position.x < targetX)
        {
            transform.position = new Vector3(transform.position.x + (10.0f * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if (transform.position.x > targetX)
        {
            transform.position = new Vector3(transform.position.x - (10.0f * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if (transform.position.y < targetY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (10.0f * Time.deltaTime), transform.position.z);
        }
        if (transform.position.y > targetY)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y - (10.0f * Time.deltaTime), transform.position.z);
        }
        //deactivate triggers when the camera is panning
        if (transform.position.y != targetY && transform.position.x != targetX)
        {
            left.GetComponent<CameraTrigger>().inactive = true;
            right.GetComponent<CameraTrigger>().inactive = true;
            up.GetComponent<CameraTrigger>().inactive = true;
            down.GetComponent<CameraTrigger>().inactive = true;
        }
        //activate triggers
        if (transform.position.y == targetY && transform.position.x == targetX)
        {
            left.GetComponent<CameraTrigger>().inactive = false;
            right.GetComponent<CameraTrigger>().inactive = false;
            up.GetComponent<CameraTrigger>().inactive = false;
            down.GetComponent<CameraTrigger>().inactive = false;
        }
        //round position if it's less than 1 difference
        if(Mathf.Abs(transform.position.y - targetY) <= 0.1)
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
        //the same but for the x position
        if (Mathf.Abs(transform.position.x - targetX) <= 0.1)
        {
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
        */
    }
    public void ChangeTargetPos(string str)
    {
        Debug.Log("Logged " + str);
        
        if (str == "down")
        {
            targetY -= 8;
        }
        if (str == "up")
        {
            targetY += 8;
        }
        if (str == "left")
        {
            targetX -= 18;
        }
        if (str == "right")
        {
            targetX += 18;
        }
    }
}
