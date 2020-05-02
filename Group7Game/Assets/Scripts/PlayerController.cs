using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;//used to move the player
    public float speed = 1.0f; //the speed of the player
    public float jumpHeight = 5f; // determines how high the player can jump
    private bool isMovingStone = false; // used to check to see if the player is moving a rune stone
    private bool stoneIsToRight = false; // used to check if the stone is to the right of the player or to the left of the player
    private int checkPoint = 0; // used to check which checkpoint the player is at
    private bool isOnLadder = false; // used to check if the player is currently on the ladder
    private int numGroundTouch;// used to check if the player is on the ground
    private GameObject interactable = null;//used to check which game object is currently selected for interaction
    public Animator animator; // the animator for animations
    private float SafetyBreakTime = 0; // used to prevent dragging from breaking when momentarily off the ground
    private bool isBreaking = false; // used to prevent dragging from breaking when momentarily off the ground
    private float jumpSafetyBreakTime = 0; // used to prevent the jump animation from transitioning when momentarily off the ground
    public bool isHidden = false;
    
    

    bool[] keysPressed = new bool[1024];
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i < 1024; i++)
        {
            keysPressed[i] = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        if (rb.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);

        }
        else animator.SetBool("isWalking", false);


        if (numGroundTouch > 0)
        {
            
                animator.SetBool("isJumping", false);
        }
        else
        {
            if (jumpSafetyBreakTime >= 0.3f)
            {
                animator.SetBool("isJumping", true);
                jumpSafetyBreakTime = 0;
            }
        }

        if (isOnLadder == true)
        {
            animator.SetBool("isClimbing", true);
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }


        animator.SetBool("isDragging", isMovingStone);

        if(isMovingStone == true && numGroundTouch == 0)
        {
            isBreaking = true;
        }
        else
        {
            isBreaking = false;
        }

        if (isBreaking == true)
        {
            SafetyBreakTime += Time.deltaTime;
        }
        else
        {
            SafetyBreakTime = 0;
        }

        if (SafetyBreakTime >= 0.3f)
        {
            Destroy(interactable.GetComponent<DraggedObject>().GetDJ());
            Destroy(interactable.GetComponent<DraggedObject>().GetRB());
            print("wtf3");
            isMovingStone = false;
            isBreaking = false;
            SafetyBreakTime = 0;
        }

        if (numGroundTouch == 0)
        {
            jumpSafetyBreakTime += Time.deltaTime;
        }
        else
        {
            jumpSafetyBreakTime = 0;
        }

        if(isMovingStone == true && stoneIsToRight == true)
        {
            if(rb.velocity.x > 0.2f)
            {
                animator.SetBool("isPushing", false);
            }
            else if(rb.velocity.x < -0.2f)
            {
                animator.SetBool("isPushing", true);
            }
        }
        else if(isMovingStone == true && stoneIsToRight == false)
        {
            if (rb.velocity.x > 0.2f)
            {
                animator.SetBool("isPushing", true);
            }
            else if (rb.velocity.x < -0.2f)
            {
                animator.SetBool("isPushing", false);
            }
        }

        GetKeyInputs();

        Animate();
    }

    //handles key inputs to prevent bugs
    void GetKeyInputs()
    {
        if(Input.GetKey(KeyCode.A))
        {
            keysPressed[(int)KeyCode.A] = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            keysPressed[(int)KeyCode.D] = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            keysPressed[(int)KeyCode.W] = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            keysPressed[(int)KeyCode.S] = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            keysPressed[(int)KeyCode.Space] = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            keysPressed[(int)KeyCode.A] = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            keysPressed[(int)KeyCode.D] = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            keysPressed[(int)KeyCode.W] = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            keysPressed[(int)KeyCode.S] = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            keysPressed[(int)KeyCode.Space] = false;
        }
    }

    void Animate()
    {

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //used to move the player
    void MovePlayer()
    {
        bool isHoldingA = false;
        bool isHoldingD = false;

        if (isOnLadder == false)
        {
            //move left
            if (keysPressed[(int)KeyCode.A])
            {
                rb.AddForce(Vector3.left * speed);
                if (isMovingStone == false)
                {
                    transform.localScale = new Vector3(-0.3f, 0.3f, 1);
                }
                
                isHoldingA = true;
            }
            //move right
            if (keysPressed[(int)KeyCode.D])
            {
                rb.AddForce(Vector3.right * speed);
                if (isMovingStone == false)
                {
                    transform.localScale = new Vector3(0.3f, 0.3f, 1);
                }
                isHoldingD = true;
            }
            //jump
            if (keysPressed[(int)KeyCode.W])
            {
                if (isMovingStone == false && numGroundTouch > 0)
                {
                    animator.SetTrigger("takeOff");
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0);
                }
            }
            //Used to limit speed
            if (rb.velocity.x > 5f)
            {
                rb.velocity = new Vector3(5f, rb.velocity.y, 0);
            }
            else if (rb.velocity.x < -5f)
            {
                rb.velocity = new Vector3(-5f, rb.velocity.y, 0);
            }
        }
        else
        {
            
            if (keysPressed[(int)KeyCode.W])
            {
                transform.position += new Vector3(0, 2.5f, 0) * Time.deltaTime;
            }
            if (keysPressed[(int)KeyCode.S])
            {
                transform.position += new Vector3(0, -2.5f, 0) * Time.deltaTime;
            }
        }
        //stops player
        if (!keysPressed[(int)KeyCode.A] && isHoldingD == false && numGroundTouch > 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (!keysPressed[(int)KeyCode.D] && isHoldingA == false && numGroundTouch > 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

    }

    void Interact()
    {
        //used for all interactable objects
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //used for bug fixing
            if (interactable == null)
            {
                print("No interactable");
            }

            //Creates a rigidbody and a distance joint on the runestone object for dragging
            else if (interactable.tag == "RuneStone")
            {
                //if the player isnt moving a stone, adds the components

                if (isMovingStone == false)
                {
                    interactable.GetComponent<DraggedObject>().createDraggingComponents();
                    if(transform.position.x > interactable.transform.position.x)
                    {
                        stoneIsToRight = true;
                        transform.localScale = new Vector3(-0.3f, 0.3f, 1);
                    }
                    else
                    {
                        stoneIsToRight = false;
                        transform.localScale = new Vector3(0.3f, 0.3f, 1);
                    }
                    isMovingStone = true;
                }
                //if the player is moving a stone, destroys the components
                else
                {
                    Destroy(interactable.GetComponent<DraggedObject>().GetDJ());
                    Destroy(interactable.GetComponent<DraggedObject>().GetRB());
                    isMovingStone = false;
                }
            }
            //Creates a rigidbody and a distance joint on the launchable object for dragging
            else if (interactable.tag == "Launchable")
            {
                if (isMovingStone == false)
                {
                    interactable.GetComponent<DraggedObject>().createDraggingComponents();
                    if (transform.position.x > interactable.transform.position.x)
                    {
                        stoneIsToRight = true;
                        transform.localScale = new Vector3(-0.3f, 0.3f, 1);
                    }
                    else
                    {
                        stoneIsToRight = false;
                        transform.localScale = new Vector3(0.3f, 0.3f, 1);
                    }
                    isMovingStone = true;
                }
                //if the player is moving a stone, destroys the components
                else
                {
                    Destroy(interactable.GetComponent<DraggedObject>().GetDJ());
                    Destroy(interactable.GetComponent<DraggedObject>().GetRB());
                    isMovingStone = false;
                }
            }
            //Interaction to climb the ladder
            else if (interactable.tag == "Ladder")
            {
                if (isOnLadder == false)
                {
                    isOnLadder = true;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.gravityScale = 0f;
                    transform.position = new Vector3(interactable.transform.position.x, transform.position.y, 0);
                }
                else
                {
                    isOnLadder = false;
                    rb.gravityScale = 1.5f;
                }
            }
            //Interaction to fire the catapult
            else if (interactable.tag == "Catapult")
            {
                interactable.GetComponent<Catapult>().LaunchCatapult();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            checkPoint = collision.GetComponent<CheckPoint>().checkPointNum;
        }

        if (collision.gameObject.tag == "Ladder" && isMovingStone == false)
        {
            interactable = collision.gameObject;
        }

        if (collision.gameObject.tag == "Catapult" && isMovingStone == false)
        {
            interactable = collision.gameObject;
        }

        //stops player being reset when touched by dragons breath
        if (collision.gameObject.tag == "HideObject")
        {
            isHidden = true;
        }

        //resets player if in contact with dragons breath
        if (collision.gameObject.tag == "DragonsBreath")
        {
            if (isHidden == false)
            {
                GameObject.Find("CheckPointManager").GetComponent<CheckPointManager>().resetPuzzle();
                print("reset");
            }
            print("passed");
        }

        //resets player if in contact with death objects
        if (collision.gameObject.tag == "Death")
        {
                GameObject.Find("CheckPointManager").GetComponent<CheckPointManager>().resetPuzzle();
                print("reset");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Pushes the player up if they are at the top of the ladder and drops the player if they are at the bottom
        if (collision.gameObject.tag == "Ladder" && isMovingStone == false)
        {
            interactable = null;
            if (isOnLadder == true && transform.position.y > collision.transform.position.y)
            {
                rb.gravityScale = 1.5f;
                rb.velocity = new Vector3(rb.velocity.x, 5f, 0);
                isOnLadder = false;
            }
            else if (isOnLadder == true && transform.position.y < collision.transform.position.y)
            {
                rb.gravityScale = 1.5f;
                isOnLadder = false;
            }
        }
        //makes the interactable null if the player leaves the catapult triggerbox
        if (collision.gameObject.tag == "Catapult" && isMovingStone == false)
        {
            interactable = null;
        }
        if (collision.gameObject.tag == "HideObject")
        {
            isHidden = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //enables the player to jump again when touching the ground
        if (collision.gameObject.tag == "Ground")
        {
            numGroundTouch++;
        }
        //enables the player to jump and makes the runestone the active interactable object
        if (collision.gameObject.tag == "RuneStone" && isOnLadder == false)
        {
            interactable = collision.gameObject;
            numGroundTouch++;
        }
        //enables the player to jump and makes the launchable the active interactable object
        if (collision.gameObject.tag == "Launchable" && isOnLadder == false)
        {
            interactable = collision.gameObject;
            numGroundTouch++;
        }
    }




    private void OnCollisionExit2D(Collision2D collision)
    {
        //interactable is removed thereby stopping functionality when not touching
        if (collision.gameObject.tag == "RuneStone")
        {
            numGroundTouch--;
            if (interactable.tag == "RuneStone" && isMovingStone == false)
            {
                interactable = null;
            }
        }

        if (collision.gameObject.tag == "Launchable")
        {
            numGroundTouch--;
            if (interactable.tag == "Launchable" && isMovingStone == false)
            {
                interactable = null;
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            numGroundTouch--;
        }

    }

    //recreates the rigidbody
    void createRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.mass = 1.0f;
        rb.gravityScale = 1.5f;
    }

    //getters/setters
    public bool getIsMovingStone()
    {
        return isMovingStone;
    }

    public void setIsMovingStone(bool newIsMovingStone)
    {
        isMovingStone = newIsMovingStone;
    }

    public GameObject getInteractable()
    {
        return interactable;
    }

    public void setInteractable(GameObject newInteractable)
    {
        interactable = newInteractable;
    }

    public void SetRB(Rigidbody2D newRB)
    {
        rb = newRB;
    }

    public Rigidbody2D GetRB()
    {
        return rb;
    }

    public int GetCheckPoint()
    {
        return checkPoint;
    }

    public void SetCheckPoint(int newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }

    public GameObject GetInteractable()
    {
        return interactable;
    }
}
