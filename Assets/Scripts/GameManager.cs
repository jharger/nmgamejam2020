using System;
using System.Collections;
using System.Collections.Generic;
using GodComplex.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private Animator fadeAnimator = default;
    [SerializeField] private Text timerText = default;
    [SerializeField] private float winWaitTime = 4;

    private static readonly int FadeInParam = Animator.StringToHash("FadeIn");
    private static readonly int FadeOutParam = Animator.StringToHash("FadeOut");
    private bool _waitingForFadeOut = false;
    private bool _waitingForFadeIn = false;
    private float _elapsedTime;
    private bool _hasWon;
    public bool HasStarted { get; private set; }

    private void Start() {
        StartGame();
    }

    private void Update() {
        // TODO remove
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        }

        // Cheat to instantly win
        /*if (Input.GetKeyDown(KeyCode.T)) {
            CueScoreScreen();
        }*/

        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }

        if (_hasWon == false) {
            _elapsedTime += Time.deltaTime;
        }
        if (timerText) {
            var t = TimeSpan.FromSeconds(_elapsedTime);
            var text = string.Empty;
            if (t.Hours > 0) {
                text = t.ToString(@"hh\:mm\:ss\.ff");
            }
            else if (t.Minutes > 0) {
                text = t.ToString(@"mm\:ss\.ff");
            }
            else {
                text = t.ToString(@"ss\.ff");
            }

            timerText.text = text;


            //ScoreManager.Instance.SetTimerText(text);
        }
    }

    public void FadeIn() {
        fadeAnimator.SetTrigger(FadeInParam);
    }

    public void FadeOut() {
        fadeAnimator.SetTrigger(FadeOutParam);
    }

    public IEnumerator FadeIn_CR() {
        fadeAnimator.SetTrigger(FadeInParam);
        _waitingForFadeIn = true;
        while (_waitingForFadeIn) {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut_CR() {
        fadeAnimator.SetTrigger(FadeOutParam);
        _waitingForFadeOut = true;
        while (_waitingForFadeOut) {
            yield return new WaitForEndOfFrame();
        }
    }

    public void FadeInFinished() {
        _waitingForFadeIn = false;
    }

    public void FadeOutFinished() {
        _waitingForFadeOut = false;
    }

    public void StartGame() {
        StartCoroutine(StartGame_CR());
    }

    private IEnumerator StartGame_CR() {
        if (SceneManager.sceneCount < 2) {
            yield return SceneManager.LoadSceneAsync("MainLevel", LoadSceneMode.Additive);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CameraManager.Instance.SetNormalCamera();

        yield return StartCoroutine(FadeIn_CR());

        if (!HasStarted) {
            WitchSoundManager.Instance.PlayIntroSound();
            HasStarted = true;
        }

        _elapsedTime = 0f;
        _hasWon = false;
    }

    public void RestartLevel() {
        WitchSoundManager.Instance.PlaySnark();

        var bodies = GameObject.FindWithTag("Player")
            .transform.root.GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies) {
            body.collisionDetectionMode = CollisionDetectionMode.Discrete;
            body.isKinematic = true;
        }

        StartCoroutine(ResetLevel_CR());
    }

    public void WinLevel() {
        StartCoroutine(WinLevel_CR());
    }

    private IEnumerator WinLevel_CR() {
        _hasWon = true;

        yield return new WaitForSeconds(0.05f);

        var bodies = GameObject.FindWithTag("Player")
            .transform.root.GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies) {
            body.collisionDetectionMode = CollisionDetectionMode.Discrete;
            body.isKinematic = true;
        }

        CameraManager.Instance.SetWinCamera();

        yield return new WaitForSeconds(winWaitTime);

        yield return StartCoroutine(FadeOut_CR());

        CueScoreScreen();
    }

    public void QuitGame() {
        StartCoroutine(QuitGame_CR());
    }

    public IEnumerator QuitGame_CR() {
        yield return StartCoroutine(FadeOut_CR());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return SceneManager.LoadSceneAsync("MainMenu");
    }

    private IEnumerator ResetLevel_CR() {
        yield return StartCoroutine(FadeOut_CR());
        var witchSoundManager = WitchSoundManager.Instance;
        yield return new WaitUntil((() => !witchSoundManager.IsPlayingVoice));
        yield return SceneManager.UnloadSceneAsync("MainLevel");
        yield return SceneManager.LoadSceneAsync("MainLevel", LoadSceneMode.Additive);
        _elapsedTime = 0f;
        _hasWon = false;
        yield return StartCoroutine(FadeIn_CR());
    }

    //TODO Debbie added these methods:
    public float GetElapsedTime() {
        return _elapsedTime;
    }

    public void CueScoreScreen()
    {
        SceneManager.UnloadSceneAsync("MainLevel");
        SceneManager.LoadSceneAsync("ScoreSummaryScene", LoadSceneMode.Additive);
    }
}
