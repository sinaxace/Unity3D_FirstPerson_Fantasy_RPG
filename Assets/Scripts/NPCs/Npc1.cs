using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code typed from this tutorial: https://youtu.be/oRPNWBvfsQk?list=PLE70FML1U9svBs9GBdhvesMkd0GuprFho
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Npc1 : MonoBehaviour
    {
        // we are going to attach this script to a Box Collider to know when the player interacts with the NPC
        public GameObject triggerText;
        public GameObject DialogObject;
        public RigidbodyFirstPersonController rigid;
        public bool hasTalked = false; // to save whether we talked to NPC or not.
        public bool isInDialogue = false;

        // we're using this method to setup a trigger to stop and talk with the NPC when pressing E.
        // This is considered a key event method in unity.
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && !isInDialogue)
            {
                triggerText.SetActive(true); 
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isInDialogue = true;

                    if (!hasTalked)
                    {

                        // DO dialogue stuff.
                        // we're going to reference the game object that contains PlayerData script (which would only be the player (in time stamp 13:53)
                        other.gameObject.GetComponent<PlayerData>().dialogueNumber = 1;
                        DialogObject.SetActive(true);
                        rigid.enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        triggerText.SetActive(false);
                    }
                    else
                    {
                        // if we have talked to them, set dialog number to 1.5f which means it is not going to be a full new dialogue
                        // instead it will be what it will display for the NPC after accepting the quest.
                        other.gameObject.GetComponent<PlayerData>().dialogueNumber = 1.5f;
                        DialogObject.SetActive(true);
                        rigid.enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        triggerText.SetActive(false);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            triggerText.SetActive(false);
        }
    }
}