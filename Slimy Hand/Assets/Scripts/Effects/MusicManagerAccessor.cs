using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerAccessor : MonoBehaviour
{
    public void PlayTrack(AudioClip track)
    {
        MusicManager.Instance.PlayLoopingTrack(track);
    }
    public void StopTrack()
    {
        MusicManager.Instance.StopLoop();
    }
}