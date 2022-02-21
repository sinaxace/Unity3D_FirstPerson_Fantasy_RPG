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

        // we're using this method to setup a trigger to stop and talk with the NPC when pressing E.
        // This is considered a key event method in unity.
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                triggerText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // DO dialogue stuff.
                    // we're going to reference the game object that contains PlayerData script (which would only be the player (in time stamp 13:53)
                    other.gameObject.GetComponent<PlayerData>().dialogueNumber = 1;
                    DialogObject.SetActive(true);
                    rigid.enabled = false;
                    Cursor.lockState = CursorLockMode.None; 
                    Cursor.visible = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            triggerText.SetActive(false);
        }
    }
}