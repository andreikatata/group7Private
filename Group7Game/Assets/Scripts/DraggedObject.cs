using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedObject : MonoBehaviour
{
    protected bool playerCollision = false; //used to prevent the rigidbody from being destroyed if the player is dragging it
    protected bool isTouchingStone = false; //used to prevent bugs where collisions break the joint
    protected bool isTouchingOtherObject = false; //used to prevent bugs where collisions break the joint
    protected bool limitControl = true; //used to check if a speed limit should be applied to the object
    public Rigidbody2D rb;//Rigidbody is dynamically created and destroyed under certain conditions
    protected DistanceJoint2D distanceJoint; //Distance joint to be added to the player when dragging
    public bool afterFrame = false;
    private float SafetyBreak = 0;
    private bool isBreaking = false;
    public bool isRound = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (afterFrame == true)
        {
            CreateRB();
            afterFrame = false;
        }

        if (isBreaking == true)
        {
            SafetyBreak += Time.deltaTime;
        }
        else
        {
            SafetyBreak = 0;
        }

        if (SafetyBreak >= 0.3f)
        {
            print("piss2");
            Destroy(distanceJoint);

            GameObject.Find("Character").GetComponent<PlayerController>().setIsMovingStone(false);
            isBreaking = false;
            SafetyBreak = 0;
        }
    }

    private void FixedUpdate()
    {
        //limits the movement of the rune stone
        if (limitControl == true)
        {
            if (rb != null)
            {
                if (rb.velocity.x > 3)
                {
                    rb.velocity = new Vector3(3f, rb.velocity.y, 0);
                }
                else if (rb.velocity.x < -3)
                {
                    rb.velocity = new Vector3(-3f, rb.velocity.y, 0);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destroys the rigidbody when colliding with the ground, but only if the player isnt touching the collider
        if (collision.gameObject.tag == "Ground")
        {
            isBreaking = false;
            if (playerCollision == false && GameObject.Find("Character").GetComponent<PlayerController>().getIsMovingStone() == false)
            {
                Destroy(rb);
                print("wtf1");
            }
        }
        //ensures the rune stone does not break the joint when colliding with another rune stone
        if (collision.gameObject.tag == "RuneStone")
        {
            isTouchingOtherObject = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //stops the joint from being destroyed if the player
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = false;
        }

        //makes the stone fall when pushed off a ledge and prevents the joint from breaking if you collide with another stone
        if (collision.gameObject.tag == "Ground" && isTouchingOtherObject == false)
        {
            isBreaking = true;
        }
        //makes it possible for the rune stone to break the joint after falling off edge, should the player touch the rune stone with another rune stone
        if (collision.gameObject.tag == "RuneStone")
        {
            isTouchingOtherObject = false;
        }

        //makes it possible for the rune stone to break the joint after falling off edge, should the player touch the rune stone with another rune stone
        if (collision.gameObject.tag == "Launchable")
        {
            isTouchingOtherObject = false;
        }


    }
    //Creates all the components necessairy for the dragging mechanic.
    public virtual void createDraggingComponents()
    {   
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.mass = 0.7f;
        rb.gravityScale = 1.5f;
        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.distance = 3.7f * transform.localScale.x;
        distanceJoint.connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetRB();
    }

    public void SetRB(Rigidbody2D newRB)
    {
        rb = newRB;
    }

    public Rigidbody2D GetRB()
    {
        return rb;
    }

    public void CreateRB()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        if(!isRound)
        {
            rb.freezeRotation = true;
        }
        
    }

    public void DestroyRB()
    {
        print("wtf2");
        Destroy(rb);
    }

    public void SetDJ(DistanceJoint2D newDistanceJoint)
    {
        distanceJoint = newDistanceJoint;
    }

    public DistanceJoint2D GetDJ()
    {
        return distanceJoint;
    }
}
