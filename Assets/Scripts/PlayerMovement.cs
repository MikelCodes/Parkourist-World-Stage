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
    private float maxSpeed = 30f;

    //is player looking to the right?
    public bool lookingRight = true;
    //is player next too a wall?
    private bool foundWall;
    //is the player on a wall?
    private bool onWall;
    //angle ontop of intial angle
    public float yAngle;

    [SerializeField]
    private Slider attachBar;
    [SerializeField]
    private float attachMaxTime;

    private float attachTime;
    private bool canAttach;
    private bool wallAhead;
    private bool jumped;
    private float jumpTimer;

    void Start()
    {
        //seting direction of raycasts, one every direction except up
        attachTime = attachMaxTime;
        attachBar.gameObject.SetActive(false);
    }

    void Update()
    {
        //makes sure player falls
        rb.AddForce(Vector3.down * 0.4f);

        //run keyPressed
        keyPressed();

        // Limiting Speed, slows you down more if your on a wall

        if (onWall == true)
        {
            if (rb.velocity.magnitude > maxSpeed/2)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed/2);
            }
        }
        else if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

        //makes player deattach from the wall
        if (rb.constraints == RigidbodyConstraints.FreezePositionY)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (jumpTimer <= 0)
        {
            jumped = false;
        }
        else
        { 
            jumpTimer -= Time.deltaTime; 
        }
        //Debug.Log(attachTime);        
    }

    //keyPressed
    void keyPressed()
    {
            //if pressing forward key
            if (Input.GetKey(forward))
        {
            if (lookingRight == false)
            {
                lookingRight = true;
                //Makes player look right
                transform.rotation = Quaternion.Euler(0, 0 + yAngle, 0);
            }
            if (wallAhead == true)
            {
                rb.AddForce(transform.forward * moveSpeed / 2);
            }
            else
            {
                rb.AddForce(transform.forward * moveSpeed);
            }

            //Debug.Log(transform.rotation.y);

        }

        //if pressing backwards key
        if (Input.GetKey(backwards))
        {
            if (lookingRight == true)
            {
                lookingRight = false;
                //Makes player look left
                transform.rotation = Quaternion.Euler(0, 180 + yAngle, 0);
            }
            if (wallAhead == true)
            {
                rb.AddForce(transform.forward * moveSpeed / 2);
            }
            else
            {
                rb.AddForce(transform.forward * moveSpeed);
            }
            //Debug.Log(transform.rotation.y);
        }

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

        //set the player to the wall
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
        }
        else
        {
            attachBar.gameObject.SetActive(false);
        }

        //stores raycast hit data in 'floor'
        RaycastHit floor;

        //if pressing jump key
        if (Input.GetKey(jump))
        {
            //if on a wall, the player can jump
            if (foundWall == true && onWall == true)
            {
                //jumps
            
                rb.AddForce(Vector3.up * jumpHeight/30);
                attachTime -= 0.001f;
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
                        jumpTimer = 0.2f;
                    }
                }
            }
        }

        if (Physics.Raycast(rb.transform.position, Vector3.down, out floor, groundDist))
        {
            if (floor.transform.tag == "floor" || floor.transform.tag == "wall")
            {
                attachTime = attachMaxTime;
            }
        }
           

    }
}
