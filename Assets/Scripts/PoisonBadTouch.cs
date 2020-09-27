using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBadTouch : MonoBehaviour {
    [SerializeField] private AudioClip sizzleClip;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(sizzleClip, transform.position);

            Debug.Log("BAD TOUCH");
        }
    }
}
