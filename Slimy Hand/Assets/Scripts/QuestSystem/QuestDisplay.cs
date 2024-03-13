using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    private Quest _quest;
    public Quest quest { get { return _quest; } set { _quest = value; questText.text = _quest.questIdentity.questInfo; } }

    [SerializeField] private TextMeshProUGUI questText;
}