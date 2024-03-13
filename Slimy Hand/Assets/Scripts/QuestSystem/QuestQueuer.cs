using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQueuer : MonoBehaviour
{
    [SerializeField] private Quest quest;

    public void StartQuest()
    {
        QuestSystem.Instance.QueueQuest(quest);
    }
}