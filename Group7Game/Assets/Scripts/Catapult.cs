using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {
    public GameObject attachedArm;//the catapult arm
    public GameObject target;//where the catapult will shoot
    private float resetTime = 0;//used to reset the catapult
    private bool hasLaunched = false;//used to check if the catapult has launched
    private bool angleCheck = false;
    public bool isFacingLeft = false;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (hasLaunched == true)
        {
            if(!isFacingLeft)
            {
                //releases the launchable object
                if (attachedArm.transform.rotation.eulerAngles.z >= 270 && attachedArm.GetComponent<CatapultArm>().getLaunchable() != null)
                {
                    angleCheck = true;
                }
                if (attachedArm.transform.rotation.eulerAngles.z <= 270 && attachedArm.GetComponent<CatapultArm>().getLaunchable() != null && angleCheck == true)
                {
                    attachedArm.GetComponent<CatapultArm>().ReleaseLaunchable(target.transform.position);
                    angleCheck = false;
                }
            }
            else
            {
                //releases the launchable object
                if (attachedArm.transform.rotation.eulerAngles.z >= 90 && attachedArm.GetComponent<CatapultArm>().getLaunchable() != null)
                {
                    attachedArm.GetComponent<CatapultArm>().ReleaseLaunchable(target.transform.position);
                }
            }
            
            //resets arm after a period of time
            resetTime += Time.deltaTime;
            if (resetTime >= 1.5)
            {
                attachedArm.GetComponent<CatapultArm>().ResetArm();
                hasLaunched = false;
                resetTime = 0;
            }
        }
    }

    //launches the catapult
    public void LaunchCatapult()
    {
        attachedArm.GetComponent<CatapultArm>().RotateArm();
        hasLaunched = true;
    }

    public bool getIsFacingLeft()
    {
        return isFacingLeft;
    }
}
