using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //making a vector 3 for direction of ground
    private Vector3 hitDir, leftHit, rightHit, forwardHit, backwardsHit;

    //is player looking to the right?
    public bool lookingRight = true;
    //is player next too a wall?
    private bool foundWall;
    //is the player on a wall?
    private bool onWall;

    //angle ontop of intial angle
    public float yAngle;

    void Start()
    {
        //seting direction of raycasts, one every direction except up
        hitDir = new Vector3(0, -90, 0);
        leftHit = new Vector3(0, 0, -90); 
        rightHit = new Vector3(0, 0, 90); 
        forwardHit = new Vector3(90, 0, 0); 
        backwardsHit = new Vector3(-90, 0, 0);
    }

    void Update()
    {
        //run keyPressed
        keyPressed();

        // Limiting Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (rb.constraints == RigidbodyConstraints.FreezePositionY)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
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
            rb.AddForce(transform.forward * moveSpeed);
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
            rb.AddForce(transform.forward * moveSpeed);
            //Debug.Log(transform.rotation.y);
        }

        foundWall = false;
        onWall = false;

        //stores raycast hit data in 'wall'
        RaycastHit wall;

        //raycasts to check if there is a wall next to the player ( can be optomised)
        if (Physics.Raycast(rb.transform.position, leftHit, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {

                foundWall = true;
            }
        }
        if (Physics.Raycast(rb.transform.position, rightHit, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
            }
        }
        if (Physics.Raycast(rb.transform.position, backwardsHit, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
            }
        }
        if (Physics.Raycast(rb.transform.position, forwardHit, out wall, groundDist))
        {
            if (wall.transform.tag == "wall")
            {
                foundWall = true;
            }
        }

        //set the player to the wall
        if (Input.GetKey(wallGrab) && foundWall == true)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            onWall = true;
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
            
                rb.AddForce(Vector3.up * jumpHeight);
            }
            //run raycast to check for ground
            else if (Physics.Raycast(rb.transform.position, Vector3.down, out floor, groundDist))
            {
                if (floor.transform.tag == "floor" || floor.transform.tag == "wall")
                {
                    //jumps

                    rb.AddForce(Vector3.up * jumpHeight);
                }
            }

        }

    }
}
