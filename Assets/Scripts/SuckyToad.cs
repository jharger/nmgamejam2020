using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SuckyToad : MonoBehaviour {
    [SerializeField] private LineRenderer tongueRenderer = default;
    [SerializeField] private Transform targetPoint = default;
    [SerializeField] private float suckSpeed = 2.5f;
    [SerializeField] private float holdTime = 1f;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private AudioClip suckSound = default;
    [SerializeField] private AudioClip croakSound = default;

    private Collider _collider;

    private void Awake() {
        _collider = GetComponent<Collider>();
        tongueRenderer.enabled = false;
        tongueRenderer.positionCount = 2;
        tongueRenderer.SetPosition(0, tongueRenderer.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(SuckEyeball(other.gameObject));
        }
    }

    private IEnumerator SuckEyeball(GameObject eyeball) {
        _collider.enabled = false;

        AudioSource.PlayClipAtPoint(suckSound, tongueRenderer.transform.position);

        var eyeballTransform = eyeball.transform.root;

        tongueRenderer.SetPosition(1, eyeballTransform.position);
        tongueRenderer.enabled = true;

        var eyeballController = eyeballTransform.GetComponentInChildren<EyeballController>();
        eyeballController.FreezeEyeball();

        while (Vector3.Distance(eyeballTransform.position, targetPoint.position) > 0.1f) {
            eyeballTransform.position = Vector3.MoveTowards(
                eyeballTransform.position,
                targetPoint.position,
                suckSpeed * Time.deltaTime
            );
            tongueRenderer.SetPosition(1, eyeballTransform.position);
            yield return new WaitForEndOfFrame();
        }

        WitchSoundManager.Instance.PlayFrogSound();

        yield return new WaitForSeconds(holdTime);

        tongueRenderer.enabled = false;

        eyeballController.UnfreezeEyeball();
        eyeballController.Shoot(targetPoint.forward);

        AudioSource.PlayClipAtPoint(croakSound, tongueRenderer.transform.position);

        yield return new WaitForSeconds(resetTime);

        _collider.enabled = true;
    }
}
