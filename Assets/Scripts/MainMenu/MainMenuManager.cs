using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Components begin
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    protected Animator mainMenuAnim;
    protected AudioSource audioSource;
    // Components end

    protected bool transitionInProgress;

    public AudioClip SquishClip;
    public AudioClip ButtonHoveredClip;
    public AudioClip ButtonClickedClip;

    public string playSceneName;

    public void Start()
    {
        // Set component references
        mainMenuAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Initialize variables
        transitionInProgress = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void ToggleCreditsScreen()
    {
        if (mainMenuPanel)
        {
            mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);


            // Set creditsPanel activeSelf to opposite of mainMenuPanel activeSelf
            if (creditsPanel)
            {
                creditsPanel.SetActive(!mainMenuPanel.activeSelf);
            }
        }
    }

    protected IEnumerator PlayGameCoroutine()
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

    protected IEnumerator QuitGameCoroutine()
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
        if(audioSource && SquishClip)
        {
            audioSource.clip = SquishClip;
            audioSource.Play();
        }
    }

    public void PlayButtonClickedSound()
    {
        if (audioSource && ButtonClickedClip)
        {
            audioSource.clip = ButtonClickedClip;
            audioSource.Play();
        }
    }

    public void PlayButtonHoveredSound()
    {
        if (audioSource && ButtonHoveredClip)
        {
            audioSource.clip = ButtonHoveredClip;
            audioSource.Play();
        }
    }
}
