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
        offset = player.GetComponent<PlayerMovement>().yAngle;
        if (offset > 180 || offset < -180)
        {
            offset /= 2;
        }

        if (offset <= 90 && offset >= -90)
        {
            z = offset/90 * -distAway;
            x = distAway - (distAway * offset /90);
        }
        else if (offset >= -90 && offset <= 90)
        {
            z = (offset * -distAway / 90);
            x = (-distAway + (-distAway * offset / 90));
        }
        else if (offset > 90)
        {
            x = -offset / 90 - 1 * distAway ;
            z = -distAway + ( -offset / 90 - 1 * distAway);
        }
        else if (offset < -90)
        {
            z = (offset/ 2 * -distAway / 90);
            x = (-distAway + (-distAway * offset / 2 / 90));
        }

        transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z + z);
        transform.LookAt(player.transform.position);
    }
}
