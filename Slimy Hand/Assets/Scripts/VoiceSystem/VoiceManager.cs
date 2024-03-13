using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;
    private List<PlayableVoice> voiceQueue = new List<PlayableVoice>();

    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private AudioSource voiceSource;

    private bool playingQueue;

    private void Awake()
    {
        Instance = this;
    }
    public void QueueVoice(PlayableVoice playableVoice)
    {
        voiceQueue.Add(playableVoice);

        if (!playingQueue)
            StartCoroutine(PlayVoices());
    }
    IEnumerator PlayVoices()
    {
        playingQueue = true;
        while (voiceQueue.Count > 0)
        {
            PlayableVoice voice = voiceQueue[0];
            dialogueBox.text = $"{voice.characterName}: {voice.transcript}";

            voiceSource.clip = voice.voiceClip;
            voiceSource.Play();

            yield return new WaitForSeconds(voice.voiceClip.length + 1.5f);

            dialogueBox.text = "";
            voiceQueue.RemoveAt(0);

            yield return null;
        }
        playingQueue = false;
    }
}