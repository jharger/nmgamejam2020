using System;
using UnityEngine;
using UnityEngine.Events;

public class FadeEvents : MonoBehaviour {
    [SerializeField] private UnityEvent fadeOutFinished;
    [SerializeField] private UnityEvent fadeInFinished;

    public void FadeOutFinished() {
        Debug.Log("FOF");
        fadeOutFinished.Invoke();
    }

    public void FadeInFinished() {
        Debug.Log("FIF");
        fadeInFinished.Invoke();
    }
}
