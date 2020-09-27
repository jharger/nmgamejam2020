using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerWinnerChickenDinner : MonoBehaviour {
    [SerializeField] private AudioClip winnerClip = default;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(winnerClip, transform.position);

            GameManager.Instance.WinLevel();
        }
    }
}
