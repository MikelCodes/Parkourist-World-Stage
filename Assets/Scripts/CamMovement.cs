using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    private float y, distAway;

    private float offset, x, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = player.GetComponent<PlayerMovement>().yAngle / 90;
        if (offset > 2 || offset < -2)
        {
            offset /= 2;
        }

        if (offset <= 1 && offset >= 0)
        {
            z = offset * -distAway;
            x = distAway - (distAway * offset);
        }
        else if (offset >= -1 && offset <= 0)
        {
            z = -(offset * distAway);
            x = -(distAway + (distAway * offset));
        }
        else if (offset > 1)
        {
            x = -offset / 2 * distAway;
            z = distAway - (distAway * -offset / 2);
        }
        else if (offset < -1)
        {
            x = offset / 2 * distAway;
            z = distAway - (distAway * offset / 2);
        }

        transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z + z);
        transform.LookAt(player.transform.position);
    }
}
