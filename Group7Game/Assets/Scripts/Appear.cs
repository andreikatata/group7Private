using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour {
    public GameObject trigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(trigger.tag == "Trigger")
        {
            if (trigger.GetComponent<Trigger>().GetIsTriggered())
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }else if(trigger.tag == "RuneStoneSlot")
        {
            if (trigger.GetComponent<RuneStoneSlot>().GetActivate() == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
		
	}
}
