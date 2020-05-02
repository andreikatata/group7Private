using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotatingPlatform : MonoBehaviour
{

    public int checkPoint;
    public int speed = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    
    {
        if (Mathf.Abs(Input.GetAxis("RotatePlatform")) > 0)
        {
            //Set a rate at which platform should turn
            float turnSpeed = speed * Time.deltaTime;
            //Connect turning rate to horizonal motion for smooth transition
            float rotate = Input.GetAxis("RotatePlatform") * -turnSpeed;
            //Get current rotation
            gameObject.transform.Rotate(Vector3.forward * rotate);
        }
    }
}
