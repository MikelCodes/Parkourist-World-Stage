using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    bool paused = false;
    // Update is called once per frame

    void Update()
    {
        //Debug.Log(Time.timeScale);
        if (Input.GetKey(KeyCode.Escape))
        {
            if (paused == false)            
            {
                Time.timeScale = 0;
                paused = true;
            }
            else if (paused == true) 
            {
                Time.timeScale = 1;
                paused = false;
            }

        }
    }
}
