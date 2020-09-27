using System;
using GodComplex.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WitchSoundManager : Singleton<WitchSoundManager> {
    [FormerlySerializedAs("clips")] [SerializeField]
    private AudioClip[] snarkClips;

    [SerializeField] private AudioClip frogClip;
    [SerializeField] private AudioSource audioSource;

    public void PlaySnark() {
        int clipIndex = Random.Range(0, snarkClips.Length);
        var clip = snarkClips[clipIndex];
        audioSource.PlayOneShot(clip);
    }

    public void PlayFrogSound() {
        audioSource.PlayOneShot(frogClip);
    }

    public bool IsPlayingVoice {
        get { return audioSource.isPlaying; }
    }
}
