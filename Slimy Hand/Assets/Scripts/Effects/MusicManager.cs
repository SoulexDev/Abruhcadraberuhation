using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource stingerSource;

    [SerializeField] private SourceSet loopingSources;

    [SerializeField] private float fadeLength = 1;

    private bool trackSwitching;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayStingerTrack(AudioClip track)
    {
        stingerSource.PlayOneShot(track);
    }
    public void PlayLoopingTrack(AudioClip clip)
    {
        if (trackSwitching)
            loopingSources.currentSourceIndex = GetNextSourceIndex(loopingSources);

        StopAllCoroutines();
        StartCoroutine(FadeTrack(clip, loopingSources));
    }
    public void StopLoop()
    {
        if (trackSwitching)
            loopingSources.currentSourceIndex = GetNextSourceIndex(loopingSources);

        StopAllCoroutines();
        StartCoroutine(FadeTrack(null, loopingSources));
    }
    private IEnumerator FadeTrack(AudioClip clip, SourceSet sourceSet)
    {
        if (clip == sourceSet.sources[sourceSet.currentSourceIndex].clip)
            yield break;

        trackSwitching = true;

        int nextSourceIndex = GetNextSourceIndex(sourceSet);

        AudioSource source = sourceSet.sources[sourceSet.currentSourceIndex];
        AudioSource nextSource = sourceSet.sources[nextSourceIndex];

        nextSource.clip = clip;

        nextSource.volume = 0;

        if (!source.isPlaying)
            source.Play();
        if (!nextSource.isPlaying)
            nextSource.Play();

        while (source.volume > 0)
        {
            float volumeValue = Time.deltaTime / fadeLength;
            nextSource.volume += volumeValue;
            source.volume -= volumeValue;
            yield return null;
        }

        source.volume = 0;
        nextSource.volume = 1;

        sourceSet.currentSourceIndex = nextSourceIndex;
        trackSwitching = false;
    }
    public int GetNextSourceIndex(SourceSet sourceSet)
    {
        return sourceSet.currentSourceIndex + 1 >= sourceSet.sources.Length ? 0 : sourceSet.currentSourceIndex + 1;
    }
}
[System.Serializable]
public class SourceSet
{
    public AudioSource[] sources;
    [HideInInspector] public int currentSourceIndex;
}