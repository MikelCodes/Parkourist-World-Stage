using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool paused = false;
    public float score;
    public float timeRemaining;

    [SerializeField]
    public int maxTime;

    [SerializeField]
    private Text tutorial;
    [SerializeField]
    private Text tutorial2;
    [SerializeField]
    private Text tutorial3;

    private bool firstTutorial = false;
    private float tempValue;

    private void Start()
    {
        timeRemaining = maxTime;
        Application.targetFrameRate = 60;
        tutorial3.enabled = false;
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
        else if (timeRemaining < maxTime - 5)
        {
            tutorial.enabled = false;
            tutorial2.enabled = false;
            
        }

        if (firstTutorial == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                firstTutorial = true;
                tutorial3.enabled = true;
                tempValue = timeRemaining;
            }
        }
        else if (timeRemaining <= tempValue - 2)
        {
            tutorial3.enabled = false;
        }


    }

    public void restart()
    {
        //Debug.Log(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
