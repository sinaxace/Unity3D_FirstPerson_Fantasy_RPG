using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// code typed from this tutorial: https://youtu.be/oRPNWBvfsQk?list=PLE70FML1U9svBs9GBdhvesMkd0GuprFho


// TODO: Currently at 23 minutes into part 11 of equipping items. Just finished equipping the dagger prefab.

/// <summary>
/// PlayerData script contains code pertaining to the player
/// </summary>
public class PlayerData : MonoBehaviour
{
    // for the RPG, we'll need a variable to store which quest the player is on atm.
    // quest number 0 is the default and it means that they're not on an actual quest.
    //public int questNumber;
    public float dialogueNumber;

    // this will store things like damage taken, character speed, etc.. kinda like in a fallout game.
    [Header("PlayerStats")]
    public float maxHealth;
    public float currentHealth;
    public TextMeshProUGUI healthText; // for displaying the actual health amount.

    public ObjectData[] HotBar;
    public ObjectData[] Inventory; // for making the hotbar interact with our inventory.
    public Image[] InventorySlots;
    public Image[] HotbarSlots;
    public Image[] BackgroundSlots; // may not need this since we are not actually going to equip any inventory slots

    public Color equippedColor;
    public Color normalColor;

    public int currentEquipped = 0; // to store which spot in the hotbar is currently equiped.

    [Header("UIComponents")]
    public Slider healthSlider;

    [HideInInspector]
    public bool isDragged;
    public int moveToSlot;
    public ObjectData itemData;
    //public int parentItem;
    public bool isInInventory; // for detecting whether the original drag item is from the inventory or the hotbar


    private Vector3 mousePosition;

    public GameObject DraggingSprite;

    private GameObject TempDragObj; // this will be the holder variable for our dragged image

    public GameObject Canvas;

    private UIManager manager;

    public GameObject cameraObj;

    private GameObject CurrentEquipped;

    // to highlight the equipped item in the hotbar
    public void EquipHotbar()
    {
        //Debug.Log("Current Equipped: " + currentEquipped);
        for (int i = 0; i < BackgroundSlots.Length; i++)
        {
            if (i == currentEquipped) // if the iterated slot is the same as the currentEquipped slot
            {
                BackgroundSlots[currentEquipped].color = equippedColor; // add the current equipped color as normal

                // here is for actually equipping the sword prefab
                if (HotBar[i] != null)
                {

                    CurrentEquipped = Instantiate(HotBar[i].weaponPrefab, transform.position, transform.rotation); // instantiating the new sword to a random position
                    CurrentEquipped.transform.SetParent(cameraObj.transform); // setting it's parent as the player's camera

                    CurrentEquipped.transform.localPosition = HotBar[i].weaponPrefab.transform.position; //  and then making the position relative to that weapon prefab's position we adjusted with the dagger.
                    CurrentEquipped.transform.rotation = HotBar[i].weaponPrefab.transform.rotation;
                } else
                {
                    // if there is nothing in the current hotbar slot equipped
                    Destroy(CurrentEquipped);
                }

                // here is for fixing that issue with the coloring not changing in the last hotbar slot
                if (currentEquipped != 0 && currentEquipped != BackgroundSlots.Length-1) // if currentEquipped is not at the beginning && currentEquipped is not at the end
                {
                    BackgroundSlots[currentEquipped - 1].color = normalColor; // for making the slot to the left normal
                    BackgroundSlots[currentEquipped + 1].color = normalColor; // for making the slot to the right normal
                } else if (currentEquipped == 0) // if the currentEquipped is at the beginning
                {
                    BackgroundSlots[BackgroundSlots.Length - 1].color = normalColor;
                    BackgroundSlots[currentEquipped+1].color = normalColor;
                } else if (currentEquipped == BackgroundSlots.Length-1)
                {
                    //Debug.Log("Reached the beginning");
                    BackgroundSlots[0].color = normalColor;
                    BackgroundSlots[BackgroundSlots.Length - 2].color = normalColor;
                }
                break;
            }
        }
    }

    public void ReloadHotbar()
    {
        for (int i = 0; i < HotBar.Length; i++)
        {
            if (HotBar[i] != null)
            {
                HotbarSlots[i].sprite = HotBar[i].sprite;
                HotbarSlots[i].color = new Color(255, 255, 255, 355);
            } else
            {
                // for deleting the item from the last hotbar slot.
                HotbarSlots[i].sprite = null;
                HotbarSlots[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    // for now, the only way for an item to be in inventory is if you drag something to it from the hotbar.
    public void ReloadInventory()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null)
            {
                InventorySlots[i].sprite = Inventory[i].sprite;
                InventorySlots[i].color = new Color(255, 255, 255, 355);
            }
            else
            {
                InventorySlots[i].sprite = null;
                InventorySlots[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    private void Update()
    {
        if (!manager.InventorySystem.activeSelf)
        {
            // if the mouse wheel is greater than 0, we're scrolling up
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                if (currentEquipped + 1 > HotBar.Length - 1)
                {
                    currentEquipped = 0;
                }
                else
                    currentEquipped++;
                EquipHotbar();
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) // if less than 0, we're scrolling down.
            {
                if (currentEquipped - 1 < 0)
                {
                    currentEquipped = HotBar.Length - 1;
                }
                else
                    currentEquipped--;
                EquipHotbar();
            }
        }

        if (TempDragObj != null)
        {
            TempDragObj.transform.position = Vector2.Lerp(TempDragObj.transform.position, Input.mousePosition, 1f);
        }
    }

    public void Start()
    {
//        Debug.Log("Hotbar Length: " + HotBar.Length);
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0"); // note that F0 eliminates decimals
        manager = FindObjectOfType<UIManager>();
        EquipHotbar();
    }

    /// <summary>
    /// This will be called during the SkeletonAI coroutine every 1.2 seconds to decrease the player health based on the damage taken.
    /// </summary>
    /// <param name="damage">The damage taken betweek minDamage and maxDamage</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        // reset the scene if current health is zero.
        if (currentHealth <= 0)
        {
            Application.LoadLevel("GameScene");
        }

        healthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0"); // note that F0 eliminates decimals
    }

    /// <summary>
    /// The Heal method is for healing the player's health pretty much.
    /// </summary>
    /// <param name="health">The health amount to be added onto the current health.</param>
    public void Heal(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0"); // note that F0 eliminates decimals
    }

    // for dragging a hotbar item
    public void DragItem(int indexNum, ObjectData passingObj, bool isInventory)
    {
        // here we are making the item icon follow the cursor.
        if (!isDragged)
        {
            if (passingObj != null)
            {
                if (!isInventory)
                {
                    isDragged = true;
                    moveToSlot = indexNum;
                    HotBar[indexNum] = null;
                    //parentItem = indexNum;
                    itemData = passingObj;
                    ReloadHotbar();
                    HotbarSlots[indexNum].gameObject.GetComponent<Slot>().objData = null;

                    TempDragObj = Instantiate(DraggingSprite, DraggingSprite.transform.position, DraggingSprite.transform.rotation);
                    TempDragObj.transform.SetParent(Canvas.transform);
                    TempDragObj.GetComponent<Image>().sprite = itemData.sprite;
                    isInInventory = false;
                } else
                {
                    isDragged = true;
                    moveToSlot = indexNum;
                    Inventory[indexNum] = null;
                    //parentItem = indexNum;
                    itemData = passingObj;
                    ReloadInventory();
                    InventorySlots[indexNum].gameObject.GetComponent<Slot>().objData = null;

                    TempDragObj = Instantiate(DraggingSprite, DraggingSprite.transform.position, DraggingSprite.transform.rotation);
                    TempDragObj.transform.SetParent(Canvas.transform);
                    TempDragObj.GetComponent<Image>().sprite = itemData.sprite;
                    isInInventory = true;
                }
            }
        }
        else
        {
            if (!isInventory)
            {
                isDragged = false;
                moveToSlot = -1;
                HotBar[indexNum] = itemData;
                //parentItem = indexNum;
                HotbarSlots[indexNum].gameObject.GetComponent<Slot>().objData = itemData;
                itemData = null;
                ReloadHotbar();
                Destroy(TempDragObj);
                TempDragObj = null;
            } else
            {
                isDragged = false;
                moveToSlot = -1;
                Inventory[indexNum] = itemData;
                //parentItem = indexNum;
                InventorySlots[indexNum].gameObject.GetComponent<Slot>().objData = itemData;
                itemData = null;
                ReloadInventory();
                Destroy(TempDragObj);
                TempDragObj = null;
            }
        }
    }

    public void ReleaseItem()
    {
        isDragged = false;
        HotBar[moveToSlot] = itemData;
        ReloadHotbar();
    }

}
