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
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //TODO fix up variables, take out 0
        float timerVal = 0;        
        //timerVal = GameManager.Instance.GetElapsedTime();
        scoreText.text = "Congrats! You ___________________ in: " + timerVal + " seconds";
    }

    // Update is called once per frame
    void Update()
    {

    }
}


