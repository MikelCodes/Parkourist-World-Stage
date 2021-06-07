using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float points;
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<GameManager>().score += points;
        Destroy(gameObject);
    }
}
