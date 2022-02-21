using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// code typed from this tutorial: https://youtu.be/oRPNWBvfsQk?list=PLE70FML1U9svBs9GBdhvesMkd0GuprFho
// after creating code for this game, you will be able to customize the dialog code for an infinite amount of dialogue options.

namespace UnityStandardAssets.Characters.FirstPerson
{


    // we're making a customizable class for the Dialogue Object properties themselves.
    // be sure to make the class Se3rializable to be edited from the inspector view.
    [Serializable]
    public class DialogueObj
    {
        public string[] dialogues; // this array contains the dialogue content
        public string characterName;
        public int questNumber;
    }

    public class DialogueObject : MonoBehaviour
    {
        [Header("Dialogue objects")]
        public DialogueObj dialogue1;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        public int currentDialogueNumber; // to store the current dialogue progression.
        private DialogueObj curDialogue = null;

        public RigidbodyFirstPersonController rigid;

        // here we are going to enable and disable the dialogue objects from the canvas and control the dialog flow.
        public PlayerData data;

        private void OnEnable()
        {
            switch (data.dialogueNumber)
            {
                case 1:
                    PlayDialogue(dialogue1);
                    curDialogue = dialogue1;
                    break;
                default:
                    break;
            }
        }

        void PlayDialogue(DialogueObj tempObj)
        {
            nameText.text = tempObj.characterName;

            if (currentDialogueNumber < tempObj.dialogues.Length)
            {
                dialogueText.text = tempObj.dialogues[currentDialogueNumber];
            }
            else
            {
                rigid.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                data.dialogueNumber = 0;
                currentDialogueNumber = 0;
                data.questNumber = curDialogue.questNumber;
                curDialogue = null;
                this.gameObject.SetActive(false);

            }

            dialogueText.text = tempObj.dialogues[0];
        }

        public void Next()
        {
            if (curDialogue != null)
            {
                currentDialogueNumber++;
                PlayDialogue(curDialogue);
            }
        }
    }
}