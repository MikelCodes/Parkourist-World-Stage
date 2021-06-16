using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool paused = false;
    public float score;
    public float timeRemaining;

    [SerializeField]
    public int maxTime;

    private void Start()
    {
        timeRemaining = maxTime;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        //Debug.Log(Time.timeScale);
        if (Input.GetKeyDown(KeyCode.Tab))
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
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            restart();
        }
    }

    public void restart()
    {
        //Debug.Log(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
