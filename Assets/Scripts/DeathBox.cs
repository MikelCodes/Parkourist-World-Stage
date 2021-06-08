using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<GameManager>().restart();
        }
    }
}
