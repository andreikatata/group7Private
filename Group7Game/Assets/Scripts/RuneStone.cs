using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : DraggedObject {
    Vector3 origonalLocation; //Used to store the origonal location of the rune stone for teleportation
    public int identifier = 0; //identifier used to check if the rune stone is associated with the rune stone slot
    public int checkPoint = 0; //used to identify which checkpoint is associated with this runestone

	// Use this for initialization
	void Start () {
        origonalLocation = transform.position;
        rb = gameObject.AddComponent<Rigidbody2D>();
    }
	


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if the rune stone identifier matches the rune stone slots identifier, the rune stone will fit into the rune stone slot, otherwise it will be teleported back to its origonal location, 
        also sets the activate variable to true once the right runestone has collided.
        */
        if (collision.gameObject.tag == "RuneStoneSlot")
        {
            
            GameObject.Find("Character").GetComponent<PlayerController>().setIsMovingStone(false);
            if (collision.gameObject.GetComponent<RuneStoneSlot>().identifier == identifier)
            {
                Destroy(distanceJoint);
                Destroy(rb);
                transform.position = collision.gameObject.transform.position + new Vector3(0,0.4f,0);
                collision.gameObject.GetComponent<RuneStoneSlot>().SetActivate(true);
            }
            else
            {
                transform.position = origonalLocation;
            }
        }
    }

    public Vector3 getOrigonalPosition()
    {
        return origonalLocation;
    }
}
