using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAppearDisappear : MonoBehaviour {
    public bool disappear = false;
    public GameObject DragonBar;
    public GameObject DragonBreath;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!disappear)
        {
            DragonBar.SetActive(true);
            DragonBreath.SetActive(true);
        }
        else
        {
            DragonBar.SetActive(false);
            DragonBreath.SetActive(false);
        }
    }

}
