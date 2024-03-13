using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFootstepPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public void PlayFootstep()
    {
        //AudioManager.Instance.PlayRandomFromArray(source, AudioManager.Instance.chatterBoxFootsteps);
    }
}