using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityStandardAssets;

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
        public DialogueObj dialogue1point5;

        [Header("NPCS")]
        public Npc1 npc1;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        public int currentDialogueNumber; // to store the current dialogue progression.
        private DialogueObj curDialogue = null;



        public RigidbodyFirstPersonController rigid;

        // here we are going to enable and disable the dialogue objects from the canvas and control the dialog flow.
        public PlayerData data;

        public void Start()
        {
            //data = FindObjectOfType<PlayerData>();
            //dialogueText = FindObjectOfType<TextMeshProUGUI>().GetComponent()
        }

        private void OnEnable()
        {
            switch (data.dialogueNumber)
            {
                case 1:
                    PlayDialogue(dialogue1);
                    curDialogue = dialogue1;
                    break;
                case 1.5f:
                    PlayDialogue(dialogue1point5);
                    curDialogue = dialogue1point5;
                    break;
                default:
                    break;
            }
        }

        void PlayDialogue(DialogueObj tempObj)
        {
            nameText.text = tempObj.characterName;
            //Debug.Log(tempObj.dialogues[currentDialogueNumber]);
            if (currentDialogueNumber < tempObj.dialogues.Length)
            {
                //Debug.Log(dialogueText.text);

                // Debug.Log("currentDialogueNumber is less than the dialogues length.");
                dialogueText.text = tempObj.dialogues[currentDialogueNumber];
            }
            else
            {
                rigid.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                switch (data.dialogueNumber)
                {
                    case 1:
                        npc1.hasTalked = true; // when quest dialogue number is 1, the npc has been talked to.
                        npc1.isInDialogue = false;
                        break;
                    case 1.5f:
                        npc1.isInDialogue = false;
                        break;
                    // in the future if there are 4 npcs for example, there can be 4 cases here to check to see if they have been talked to.
                }

                data.dialogueNumber = 0;
                currentDialogueNumber = 0;
                data.questNumber = curDialogue.questNumber;
                curDialogue = null;
                this.gameObject.SetActive(false);
                dialogueText.text = tempObj.dialogues[0];

            }

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