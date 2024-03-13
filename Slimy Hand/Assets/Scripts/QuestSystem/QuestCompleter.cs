using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    [SerializeField] private QuestIdentity questId;

    public void EndQuest()
    {
        QuestSystem.Instance.CompleteQuest(questId);
    }
}