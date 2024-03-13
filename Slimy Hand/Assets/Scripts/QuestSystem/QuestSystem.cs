using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;
    private List<Quest> quests = new List<Quest>();

    private List<QuestIdentity> preCompletedQuests = new List<QuestIdentity>();

    [SerializeField] private Transform questContainer;
    [SerializeField] private Transform markerContainer;
    [SerializeField] private QuestDisplay questDisplay;
    [SerializeField] private QuestMarker marker;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsQuestComplete(Quest quest)
    {
        if (preCompletedQuests.Contains(quest.questIdentity))
        {
            preCompletedQuests.Remove(quest.questIdentity);
            return true;
        }
        return false;
    }
    public void QueueQuest(Quest quest)
    {
        quest = new Quest(quest);

        if (preCompletedQuests.Contains(quest.questIdentity))
        {
            preCompletedQuests.Remove(quest.questIdentity);
            return;
        }

        quests.Add(quest);

        QuestDisplay newQuestDisplay = Instantiate(questDisplay, questContainer);
        newQuestDisplay.quest = quest;

        quest.associatedDisplay = newQuestDisplay.gameObject;

        if (!quest.hasPosition)
            return;

        QuestMarker newQuestMarker = Instantiate(marker, markerContainer);
        newQuestMarker.Init(quest.pos);

        quest.associatedMarker = newQuestMarker;
    }
    public void CompleteQuest(QuestIdentity questIdentity)
    {
        if (!quests.Any(q => q.questIdentity == questIdentity))
        {
            if (preCompletedQuests.Any(q => q == questIdentity))
                return;

            preCompletedQuests.Add(questIdentity);
            return;
        }

        Quest foundQuest = quests.First(q => q.questIdentity == questIdentity);

        quests.Remove(foundQuest);
        Destroy(foundQuest.associatedDisplay.gameObject);

        if(foundQuest.hasPosition)
            Destroy(foundQuest.associatedMarker.gameObject);
    }
}

[System.Serializable]
public class Quest
{
    public QuestIdentity questIdentity;
    public Transform targetObj;
    public bool hasPosition = true;

    [HideInInspector] public Vector3 pos;
    [HideInInspector] public QuestMarker associatedMarker;
    [HideInInspector] public GameObject associatedDisplay;

    public Quest(Quest quest)
    {
        questIdentity = quest.questIdentity;
        hasPosition = quest.hasPosition;

        if(hasPosition && quest.targetObj != null)
            pos = quest.targetObj.position;
    }
}