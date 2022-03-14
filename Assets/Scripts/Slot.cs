using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO: Fix error with dragging and removing original item in Part 10 

// for detecting when we have clicked or dragged somthing.
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    // we need to store what UI and what we're clicking on first.
    public int slotNumber; // for the PlayerData script to use.

    public ObjectData objData; // the data we're picking up.

    private PlayerData data;

    void Awake()
    {
        data = FindObjectOfType<PlayerData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        data.DragItem(slotNumber, objData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (data.isDragged)
        {
            data.ChangeSlots(slotNumber);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        data.ReleaseItem();
    }
}
