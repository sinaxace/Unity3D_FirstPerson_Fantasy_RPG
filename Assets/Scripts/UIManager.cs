using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this will make it so that it sets other UI to inactive whenever the dialogue box appears for example.
public class UIManager : MonoBehaviour
{
    public GameObject DialogueObj;
    public GameObject HotbarObj;

    public GameObject InventorySystem;

    private void Update()
    {
        if (DialogueObj.activeSelf)
            HotbarObj.SetActive(false);
        else
            HotbarObj.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            InventorySystem.SetActive(!InventorySystem.activeSelf);

            if (InventorySystem.activeSelf)
            {
                Time.timeScale = 0; // stop time when pressing tab
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; // make it so we can't move our mouse when pressing tab
            } else
            {
                Time.timeScale = 1; // stop time when pressing tab
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Locked; // make it so we can't move our mouse when pressing tab
            }

        }
    }


}
