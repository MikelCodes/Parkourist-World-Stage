using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    //veriables
    [SerializeField]
    private Text timerText;

    private GameManager gm;
    private int time;
    private float colour;

    // Start is called before the first frame update
    void Start()
    {
        //get gm and set time
        gm = this.GetComponent<GameManager>();
        time = gm.maxTime;
        timerText.text = time + "";
    }

    // Update is called once per frame
    void Update()
    {
        //update text veriable
        if (gm.timeRemaining < time)
        {
            time--;
            timerText.text = time + "";
        }

        //change colour of text
        colour = gm.timeRemaining / gm.maxTime;
        timerText.color = new Color(-colour + 1, colour, 0);
    }
}
