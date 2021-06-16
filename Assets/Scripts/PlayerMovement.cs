using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    //getting key binds

    //forward key
    [SerializeField]
    KeyCode forward = KeyCode.D;
    //backwards key
    [SerializeField]
    KeyCode backwards = KeyCode.A;
    //jump key
    [SerializeField]
    KeyCode jump = KeyCode.W;
    //wall climb key
    [SerializeField]
    KeyCode wallGrab = KeyCode.Space;    
    // the key to perform a trick
    [SerializeField]
    KeyCode trickKey = KeyCode.RightShift;

    //--------------------------------------------------------------------------------------------------------------------------

    //reference to rigidbody
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    //distance of raycast
    [SerializeField]
    private float groundDist;
    //seting the height of the jump
    [SerializeField]
    private float jumpHeight = 1600;
    //setting the maximum speed of any character
    [SerializeField]
    private float maxSpeed = 40f;

    //is player looking to the right?
    public bool lookingRight = true;
    //is player next too a wall?
    private bool foundWall;
    //is the player on a wall?
    public bool onWall;
    //angle ontop of intial angle
    public float yAngle;

    //--------------------------------------------------------------------------------------------------------------------------

    //veriables for wall riding limitations
    [SerializeField]
    private Slider attachBar;
    [SerializeField]
    private float attachMaxTime;

    public float attachTime;

    //safeguard against sticking to a wall
    private bool wallAhead;

    //makes sure that jump hight can't be highter then set by preventing player from jumping once a frame
    private bool jumped;
    private float jumpTimer;
    private float wallJumpTimer;

    //--------------------------------------------------------------------------------------------------------------------------

    //for the inputs to be stored and exported
    public string[] moveBank = new string[8];

    //to prevent double inputs to rapidly, I am using a float switch.
    private float arrayCooldown;

    // get game manager
    private GameManager gm;
    //--------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        gm = this.gameObject.GetComponent<GameManager>();
        //seting direction of raycasts, one every direction except up
        attachTime = attachMaxTime;
        attachBar.gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        //resets rotation when it doesn't matter
        if (yAngle/360 == 1 || yAngle/-360 == 1)
        {
            yAngle = 0;
        }
        //--------------------------------------------------------------------------------------------------------------------------

        //run keyPressed
        keyPressed();

        //--------------------------------------------------------------------------------------------------------------------------

        //makes player deattach from the wall
        if (rb.constraints == RigidbodyConstraints.FreezePositionY)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        //prevents player from jumping once a frame
        if (jumpTimer <= 0)
        {
            jumped = false;
        }
        else
        { 
            jumpTimer -= Time.deltaTime;
        }
        //Debug.Log(attachTime);    

        //--------------------------------------------------------------------------------------------------------------------------

        // Limiting Speed, slows you down more if your in the air/ wall running

        if (onWall == true)
        {
            if (rb.velocity.magnitude > maxSpeed / 1.5f)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed / 2);
            }
        }
        else if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        gravity(2);

        //--------------------------------------------------------------------------------------------------------------------------

        arrayCooldown -= Time.deltaTime;
        wallJumpTimer -= Time.deltaTime;
    }

    //--------------------------------------------------------------------------------------------------------------------------

    //keyPressed
    void keyPressed()
    {
        //--------------------------------------------------------------------------------------------------------------------------

        //if pressing forward key
        if (Input.GetKey(forward))
        {
            if (lookingRight == false)
            {
                lookingRight = true;
                //Makes player look right
                transform.rotation = Quaternion.Euler(0, 0 + yAngle, 0);
            }
            //safeguard against sticking to a wall
            if (wallAhead == true)
            {
                rb.AddForce(transform.forward * moveSpeed / 2 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
            }

            //Debug.Log(transform.rotation.y);


            if (moveBank[0] != "right" || arrayCooldown <= 0)
            {
                listUpdate("right");
                arrayCooldown = 1;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        //if pressing backwards key
        if (Input.GetKey(backwards))
        {
            if (lookingRight == true)
            {
                lookingRight = false;
                //Makes player look left
                transform.rotation = Quaternion.Euler(0, 180 + yAngle, 0);
            }

            //safeguard against sticking to a wall
            if (wallAhead == true)
            {
                rb.AddForce(transform.forward * moveSpeed / 2 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
            }
            //Debug.Log(transform.rotation.y);


            if (moveBank[0] != "left" || arrayCooldown <= 0)
            {
                listUpdate("left");
                arrayCooldown = 1;
            }
            
        }

        //--------------------------------------------------------------------------------------------------------------------------

        foundWall = false;
        onWall = false;
        
        //stores raycast hit data in 'wall'
        RaycastHit wall;

        //raycasts to check if there is a wall next to the player ( can be optomised)
        if (Physics.Raycast(rb.transform.position, -transform.right, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {

                foundWall = true;
                wallAhead = false;
            }
        }
        if (Physics.Raycast(rb.transform.position, transform.right, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
                wallAhead = false;
            }
        }
        if (Physics.Raycast(rb.transform.position, -transform.forward, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
                wallAhead = false;
            }
        }
        if (Physics.Raycast(rb.transform.position, transform.forward, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
                wallAhead = true;
            }
        }

        //set the player to attach to the wall
        if (Input.GetKey(wallGrab) && foundWall == true)
        {
            if (attachTime >= 0)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                onWall = true;
                attachBar.gameObject.SetActive(true);
                attachTime -= Time.deltaTime;
                attachBar.value = attachTime / attachMaxTime;
            }

            if (moveBank[0] != "wallRun" && moveBank[0] != "right" && moveBank[0] != "left")
            {
                listUpdate("wallRun");
                arrayCooldown = 1;
            }
            if (arrayCooldown <= 1)
            {
                listUpdate("wallRun");
            }
            
        }
        else
        {
            attachBar.gameObject.SetActive(false);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        //stores raycast hit data in 'floor'
        RaycastHit floor;

        //if pressing jump key
        if (Input.GetKey(jump))
        {
            //if on a wall, the player can jump
            if (foundWall == true && onWall == true)
            {
                //jumps
                if (wallJumpTimer < 0)
                { 
                    rb.AddForce(Vector3.up * jumpHeight / 60, ForceMode.Impulse);
                    attachTime -= 0.015f;

                    if (moveBank[0] != "wallJump" || arrayCooldown <= 0)
                    {
                        listUpdate("wallJump");
                        arrayCooldown = 1;
                    }
                    wallJumpTimer = 0.01f;
                }
            }
            //run raycast to check for ground

            else if (Physics.Raycast(rb.transform.position, Vector3.down, out floor, groundDist))
            {
                if (floor.transform.tag == "floor" || floor.transform.tag == "wall")
                {
                    if (jumped == false)
                    {
                        //jumps

                        rb.AddForce(Vector3.up * jumpHeight);

                        jumped = true;
                        jumpTimer = 1;
                        if (moveBank[0] != "jump" || arrayCooldown <= 0)
                        {
                            listUpdate("jump");
                            arrayCooldown = 1;
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
        
        if (Physics.Raycast(rb.transform.position, Vector3.down, out floor, groundDist))
        {
                attachTime = attachMaxTime;
        }
        else
        {
            gravity(1.2f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        if (Input.GetKey(trickKey))
        {
            checkList();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------

    private void gravity(float multiplier)
    {
        rb.AddForce(-transform.up * 9.807f * Time.deltaTime * multiplier, ForceMode.Impulse);
    }

    //--------------------------------------------------------------------------------------------------------------------------

        //sets list to make verable 2 veriable 3 and 3 to 4 and so on. Adds in veriable 0 to finish off
    private void listUpdate(string value)
    {
        string tempString;

        for (int i = 6; i > -1; i--)
        {
            tempString = moveBank[i];
            moveBank[i+1] = tempString;
            //Debug.Log(moveBank[i]);
        }

        moveBank[0] = value;
    }

    //--------------------------------------------------------------------------------------------------------------------------

    //check the list for combo matches
    private void checkList()
    {
        // for 4 value combos
        for (int i = 0; i < 3; i++)
        {
            //left right left right
            if (moveBank[i] == "right" && moveBank[i+1] == "left" && moveBank[i+2] == "right" && moveBank[i+3] == "left")
            {
                gm.score += 5;
                clearArray();
            }
            //right left right jump
            else if (moveBank[i] == "jump" && moveBank[i+1] == "right" && moveBank[i+2] == "left" && moveBank[i+3] == "right")
            {
                gm.score += 8;
                clearArray();
            }
            //right wallRun wallJump right
            else if (moveBank[i] == "right" && moveBank[i + 1] == "wallJump" && moveBank[i + 2] == "wallRun" && moveBank[i + 3] == "right")
            {
                gm.score += 15;
                clearArray();
            }
            //Debug.Log(moveBank[i] + moveBank[i + 1] + moveBank[i + 2] + moveBank[i + 3]);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------

    //clear the array of all veriables, basically a combo reset
    private void clearArray()
    {
        for (int i = 7; i > -1; i--)
        {
            moveBank[i] = null;
        }
    }
}
  