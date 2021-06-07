using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 camPos;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float xOffset, yOffset, zOffset;

    private void Start()
    {

    }

    private void Update()
    {
        camPos = new Vector3(player.transform.position.x+ xOffset, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
        transform.position = camPos;
    }
}
