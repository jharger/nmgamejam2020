using System;
using System.Collections;
using System.Collections.Generic;
using GodComplex.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    private void Awake() {
        StartGame();
    }

    private void Update() {
        // TODO remove
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        }
    }

    public void StartGame() {
        if (SceneManager.sceneCount < 2) {
            SceneManager.LoadScene("MovementTest", LoadSceneMode.Additive);
        }
    }

    public void RestartLevel() {
        SceneManager.UnloadSceneAsync("MovementTest");
        SceneManager.LoadScene("MovementTest", LoadSceneMode.Additive);
    }
}
