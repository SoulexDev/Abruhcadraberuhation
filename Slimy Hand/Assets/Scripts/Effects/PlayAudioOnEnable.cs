using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void Init(AudioClip[] clips, AudioClip[] additionalClips)
    {
        AudioManager.Instance.PlayRandomFromArray(source, clips);
        AudioManager.Instance.PlayRandomFromArray(source, additionalClips);
    }
}