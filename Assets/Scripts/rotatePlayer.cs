using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlayer : MonoBehaviour
{
    [SerializeField]
    private float rotateAmount;
    private PlayerMovement pms;
    private bool hitAlready;
    private float cooldown;

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (cooldown <= 0)
            {
                pms = collision.gameObject.GetComponent<PlayerMovement>();
                if (hitAlready == false)
                {
                    pms.yAngle += rotateAmount;
                    pms.lookingRight = !pms.lookingRight;
                    hitAlready = true;
                }
                else
                {
                    pms.yAngle -= rotateAmount;
                    pms.lookingRight = !pms.lookingRight;
                    hitAlready = false;
                }
                cooldown = 1;
            }
        }
    }
}
