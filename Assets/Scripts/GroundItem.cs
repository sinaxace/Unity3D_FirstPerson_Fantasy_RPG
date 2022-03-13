using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// for detecting when we pick up the item
public class GroundItem : MonoBehaviour
{
    public TextMeshProUGUI triggerText;
    public new string name; // we have to say new string because Name is actually referencing the name of the GameObject (in this case the Collider)
    public ObjectData pickupItem;

    private PlayerData data;

    private bool isPickedUp;

    public GameObject parent; // this will be for destroying the collider it's parent cube object.

    private void Start()
    {
        data = FindObjectOfType<PlayerData>();
        isPickedUp = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPickedUp)
        {
            triggerText.gameObject.SetActive(true);
        }
        triggerText.text = "Press E to Pick Up " + name;
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < data.HotBar.Length; i++)
            {
                if (data.HotBar[i] == null)
                {
                    data.HotBar[i] = pickupItem;
                    triggerText.gameObject.SetActive(false);
                    Destroy(parent, 0.1f); // Note that you can't just destroy a game component but the game object itself.
                    isPickedUp = true;
                    i = data.HotBar.Length + 1;
                }
            }
            data.ReloadHotbar(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerText.gameObject.SetActive(false);
    }
}
