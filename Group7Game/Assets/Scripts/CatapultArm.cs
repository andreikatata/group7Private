using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultArm : MonoBehaviour {
    JointMotor2D MotorRef;
    private GameObject launchable;
    public GameObject catapult;
	// Use this for initialization
	void Start () {
        MotorRef = GetComponent<HingeJoint2D>().motor;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void RotateArm()
    {
        if (catapult.GetComponent<Catapult>().getIsFacingLeft() == false)
        {
            MotorRef.motorSpeed = 400;
            GetComponent<HingeJoint2D>().motor = MotorRef;
        }
        else
        {
            MotorRef.motorSpeed = -400;
            GetComponent<HingeJoint2D>().motor = MotorRef;
        }
        
    }

    public void ResetArm()
    {
        if (catapult.GetComponent<Catapult>().getIsFacingLeft() == false)
        {
            MotorRef.motorSpeed = -50;
            GetComponent<HingeJoint2D>().motor = MotorRef;
        }
        else
        {
            MotorRef.motorSpeed = 50;
            GetComponent<HingeJoint2D>().motor = MotorRef;
        }
        
    }

    public void StoreLaunchable(GameObject launchableRef)
    {
        launchable = launchableRef;
    }

    public void ReleaseLaunchable(Vector3 target)
    {
        Destroy(launchable.GetComponent<Launchable>().getLaunchableHinge());
        launchable.GetComponent<Launchable>().setIsFiring(true);
        launchable.GetComponent<CircleCollider2D>().enabled = true;
        launchable.GetComponent<Launchable>().launch(target);
        launchable = null;
    }

    public GameObject getLaunchable()
    {
        return launchable;
    }

    public void setLaunchable(GameObject newLaunchable)
    {
        launchable = newLaunchable;
    }
}
