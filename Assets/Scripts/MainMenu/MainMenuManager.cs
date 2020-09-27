using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string playSceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
