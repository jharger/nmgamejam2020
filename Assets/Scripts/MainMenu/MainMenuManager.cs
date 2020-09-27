using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource SquishAudioSource;

    public string playSceneName;

    public void Start()
    {
        SquishAudioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySquishSound()
    {
        if(SquishAudioSource)
        {
            Debug.Log("I am here");

            SquishAudioSource.Play();
        }
    }

}
