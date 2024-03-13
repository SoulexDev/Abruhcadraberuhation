using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceQueuer : MonoBehaviour
{
    [SerializeField] private PlayableVoice[] voices;
    public void QueueVoices()
    {
        for (int i = 0; i < voices.Length; i++)
        {
            VoiceManager.Instance.QueueVoice(voices[i]);
        }
    }
}