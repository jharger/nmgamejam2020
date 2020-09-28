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

    private AudioSource audioSource;

    public AudioClip buttonHoveredClip;
    public AudioClip buttonClickedClip;

    // Start is called before the first frame update
    void Start()
    {
        //TODO fix up variables, take out 0
        //timerVal = 0;

        audioSource = GetComponent<AudioSource>();

        timerVal = GameManager.Instance.GetElapsedTime();
        scoreText.text = "Congratulations! \n\nYou became dinner in: \n" + (int)timerVal + " seconds!";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void PlayButtonClickedSound()
    {
        if(audioSource && buttonClickedClip)
        {
            audioSource.clip = buttonClickedClip;
            audioSource.Play();
        }
    }

    public void PlayButtonHoveredSound()
    {
        if (audioSource && buttonHoveredClip)
        {
            audioSource.clip = buttonHoveredClip;
            audioSource.Play();
        }
    }
}


