using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class Quest
    {
        public string questTitle; 
        public string questObj1;
        public string questObj2;
    }

    public class QuestObject : MonoBehaviour
    {
        public GameObject questObj;
        public TextMeshProUGUI QuestTitle, QuestObjective1, QuestObjective2;

        public Quest[] questObjs; // this actually removes the need for the QuestNumber from PlayerData

        public void StartNewQuest(Quest tempObj)
        {
            QuestTitle.text = tempObj.questTitle;
            QuestObjective1.text = tempObj.questObj1;
            QuestObjective2.text = tempObj.questObj2;
            questObj.SetActive(true);
            Invoke("CloseQuest", 7f); // close the quest after 7 seconds.
        }
        public void CloseQuest()
        {
            questObj.SetActive(false);
        }

    }
}
