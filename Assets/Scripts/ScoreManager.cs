using System;
using System.Collections;
using System.Collections.Generic;
using GodComplex.Utility;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private Text scoreText = default;
    private float timerVal;
    private String timerText;

    // Start is called before the first frame update
    void Start()
    {
        //TODO fix up variables, take out 0
        //timerVal = 0;
        //timerVal = GameManager.Instance.GetElapsedTime();
        scoreText.text = "Congratulations! \n\nYou became dinner in: \n" + timerText + " seconds!";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFinalTimerVal(float timerVal)
    {
        this.timerVal = timerVal;
    }


    public void SetTimerText(String text)
    {
        timerText = text;
    }
}


