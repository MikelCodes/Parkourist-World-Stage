using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float points;
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<GameManager>().score += points;
        Destroy(gameObject);
    }
}
