using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float points;
    [SerializeField]
    private float wallTime;
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<GameManager>().score += points;
        collision.gameObject.GetComponent<PlayerMovement>().attachTime += wallTime;
        Destroy(gameObject);
    }
}
