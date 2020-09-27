using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Components begin
    protected Animator mainMenuAnim;
    protected AudioSource squishAudioSource;
    // Components end

    protected bool transitionInProgress;

    public string playSceneName;

    public void Start()
    {
        mainMenuAnim = GetComponent<Animator>();

        squishAudioSource = GetComponent<AudioSource>();

        transitionInProgress = false;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void PlayGame()
    {
        if(transitionInProgress)
        {
            return;
        }

        transitionInProgress = true;

        StartCoroutine(PlayGameCoroutine());
    }

    private IEnumerator PlayGameCoroutine()
    {
        if(mainMenuAnim)
        {
            mainMenuAnim.SetTrigger("FadeOutTrigger");
        }

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(playSceneName);
    }

    public void QuitGame()
    {
        if (transitionInProgress)
        {
            return;
        }

        transitionInProgress = true;

        StartCoroutine(QuitGameCoroutine());
    }

    private IEnumerator QuitGameCoroutine()
    {
        if (mainMenuAnim)
        {
            mainMenuAnim.SetTrigger("FadeOutTrigger");
        }

        yield return new WaitForSeconds(1.0f);

        Application.Quit();
    }

    public void PlaySquishSound()
    {
        if(squishAudioSource)
        {
            squishAudioSource.Play();
        }
    }

}
