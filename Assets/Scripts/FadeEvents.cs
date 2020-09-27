using System;
using UnityEngine;
using UnityEngine.Events;

public class FadeEvents : MonoBehaviour {
    [SerializeField] private UnityEvent fadeOutFinished = default;
    [SerializeField] private UnityEvent fadeInFinished = default;

    public void FadeOutFinished() {
        fadeOutFinished.Invoke();
    }

    public void FadeInFinished() {
        fadeInFinished.Invoke();
    }
}
