using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<FootMatAudioPack> footMatAudioPacks = new List<FootMatAudioPack>();

    [Header("Component Sounds")]
    public AudioClip pressurePlateOn;
    public AudioClip pressurePlateOff;

    public AudioClip leverOn;
    public AudioClip leverOff;

    public AudioClip buttonPress;

    public AudioClip depleteSteam;
    public AudioClip steamDepleted;

    public AudioClip inputCorrect;
    public AudioClip inputWrong;

    public AudioSource managerSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudioOnSource(AudioSource source, AudioClip clip, bool randomPitch = true)
    {
        if (clip == null || source == null)
            return;
        if (randomPitch)
            source.pitch = Random.Range(0.8f, 1.2f);
        else
            source.pitch = 1;

        source.PlayOneShot(clip);
    }
    public void PlayRandomFromArray(AudioSource source, AudioClip[] clips, bool randomPitch = true)
    {
        if (clips == null)
            return;
        PlayAudioOnSource(source, clips[Random.Range(0, clips.Length)], randomPitch);
    }
}
[System.Serializable]
public class FootMatAudioPack
{
    public WalkableMaterial mat;
    public AudioClip[] stepClips;
    public AudioClip jumpClip;
    public AudioClip landClip;
}