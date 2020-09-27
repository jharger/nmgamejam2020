using System;
using UnityEngine;
using UnityEngine.Events;

public class FadeEvents : MonoBehaviour {
    [SerializeField] private UnityEvent fadeOutFinished;
    [SerializeField] private UnityEvent fadeInFinished;

    public void FadeOutFinished() {
        fadeOutFinished.Invoke();
    }

    public void FadeInFinished() {
        fadeInFinished.Invoke();
    }
}
