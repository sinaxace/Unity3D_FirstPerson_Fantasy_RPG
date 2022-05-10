using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// for detecting when we have clicked or dragged somthing.
public class Slot : MonoBehaviour, IPointerClickHandler
{
    // we need to store what UI and what we're clicking on first.
    public int slotNumber; // for the PlayerData script to use.

    public ObjectData objData; // the data we're picking up.

    private PlayerData data;

    public bool isInventory = true; // should be false for hotbar slots

    void Awake()
    {
        data = FindObjectOfType<PlayerData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        data.DragItem(slotNumber, objData, isInventory);
    }
}
