using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;




    // Update is called once per frame
    void Update()
    {
        scoreText.text = this.gameObject.GetComponent<GameManager>().score.ToString();
    }
}
