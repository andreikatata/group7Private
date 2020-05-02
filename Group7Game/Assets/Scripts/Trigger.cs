using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    [SerializeField] private GameObject triggerObject;
    [SerializeField] private Sprite triggerSprite;
    private bool isTriggered = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == triggerObject)
        {
            isTriggered = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = triggerSprite;
        }
    }

    public bool GetIsTriggered()
    {
        return isTriggered;
    }

}
