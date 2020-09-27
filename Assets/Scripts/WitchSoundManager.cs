using System;
using GodComplex.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WitchSoundManager : Singleton<WitchSoundManager> {
    [FormerlySerializedAs("clips")] [SerializeField]
    private AudioClip[] snarkClips = default;

    [SerializeField] private AudioClip[] introClips = default;
    [SerializeField] private AudioClip frogClip = default;
    [SerializeField] private AudioSource audioSource = default;

    public void PlayIntroSound() {
        int clipIndex = Random.Range(0, introClips.Length);
        var clip = introClips[clipIndex];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySnark() {
        int clipIndex = Random.Range(0, snarkClips.Length);
        var clip = snarkClips[clipIndex];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayFrogSound() {
        audioSource.clip = frogClip;
        audioSource.Play();
    }

    public bool IsPlayingVoice {
        get { return audioSource.isPlaying; }
    }
}
