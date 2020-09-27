using System;
using System.Collections;
using System.Collections.Generic;
using GodComplex.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private Animator _fadeAnimator;

    private static readonly int FadeInParam = Animator.StringToHash("FadeIn");
    private static readonly int FadeOutParam = Animator.StringToHash("FadeOut");
    private bool waitingForFadeOut = false;
    private bool waitingForFadeIn = false;

    private void Awake() {
        StartGame();
    }

    private void Update() {
        // TODO remove
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        }
    }

    public void FadeIn() {
        _fadeAnimator.SetTrigger(FadeInParam);
    }

    public void FadeOut() {
        _fadeAnimator.SetTrigger(FadeOutParam);
    }

    public IEnumerator FadeIn_CR() {
        _fadeAnimator.SetTrigger(FadeInParam);
        waitingForFadeIn = true;
        while (waitingForFadeIn) {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut_CR() {
        _fadeAnimator.SetTrigger(FadeOutParam);
        waitingForFadeOut = true;
        while (waitingForFadeOut) {
            yield return new WaitForEndOfFrame();
        }
    }

    public void FadeInFinished() {
        Debug.Log("CALLED FIF");
        waitingForFadeIn = false;
    }

    public void FadeOutFinished() {
        Debug.Log("CALLED FOF");
        waitingForFadeOut = false;
    }

    public void StartGame() {
        StartCoroutine(StartGame_CR());
    }

    private IEnumerator StartGame_CR() {
        if (SceneManager.sceneCount < 2) {
            yield return SceneManager.LoadSceneAsync("MovementTest", LoadSceneMode.Additive);
        }

        FadeIn();
    }

    public void RestartLevel() {
        StartCoroutine(ResetLevel_CR());
    }

    private IEnumerator ResetLevel_CR() {
        yield return StartCoroutine(FadeOut_CR());
        yield return SceneManager.UnloadSceneAsync("MovementTest");
        yield return SceneManager.LoadSceneAsync("MovementTest", LoadSceneMode.Additive);
        yield return StartCoroutine(FadeIn_CR());
    }
}
