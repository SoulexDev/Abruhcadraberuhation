using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayableVoice : ScriptableObject
{
    public string characterName;
    public AudioClip voiceClip;
    [TextArea(5, 20)] public string transcript;
}