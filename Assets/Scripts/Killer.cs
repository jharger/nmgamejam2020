using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (!this.enabled) {
            return;
        }

        if (other.CompareTag("Player")) {
            this.enabled = false;
            GameManager.Instance.RestartLevel();
        }
    }
}
