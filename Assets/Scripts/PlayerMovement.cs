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
    private Vector3 hitDir;

    private bool lookingRight = true;

    [SerializeField]
    private float yAngle;

    void Start()
    {
        //seting direction of raycast directly down
        hitDir = new Vector3(0, -90, 0);
    }

    void Update()
    {
        //run keyPressed
        keyPressed();

        // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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
            Debug.Log(transform.rotation.y);
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
            Debug.Log(transform.rotation.y);
        }


        //stores raycast hit data in 'wall'
        RaycastHit wall;
        if (Input.GetKey(wallGrab))
        {
            if (Physics.Raycast(rb.transform.position, hitDir, out wall, groundDist))
            {
                if (wall.transform.tag == "wall")
                {
                    //jumps
                    rb.AddForce(transform.up * jumpHeight);
                }
            }
        }

        //if pressing jump key
        RaycastHit floor;
        if (Input.GetKey(jump))
        {
            //run raycast to check for ground
            if (Physics.Raycast(rb.transform.position, hitDir, out floor, groundDist))
            {
                if (floor.transform.tag == "floor")
                {
                    //jumps
                    rb.AddForce(transform.up * jumpHeight);
                }
            }

        }

    }
}
