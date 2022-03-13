using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// code typed from this tutorial: https://youtu.be/oRPNWBvfsQk?list=PLE70FML1U9svBs9GBdhvesMkd0GuprFho

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

    public Image[] HotbarSlots;
    public Image[] BackgroundSlots;

    public Color equippedColor;
    public Color normalColor;

    public int currentEquipped = 0; // to store which spot in the hotbar is currently equiped.

    [Header("UIComponents")]
    public Slider healthSlider;

    // to highlight the equipped item in the hotbar
    public void EquipHotbar()
    {
        //Debug.Log("Current Equipped: " + currentEquipped);
        for (int i = 0; i < BackgroundSlots.Length; i++)
        {
            if (i == currentEquipped)
            {
                BackgroundSlots[currentEquipped].color = equippedColor;

                if (currentEquipped != 0 && currentEquipped != BackgroundSlots.Length-1)
                {
                    BackgroundSlots[currentEquipped - 1].color = normalColor; // for making the slot to the left normal
                    BackgroundSlots[currentEquipped + 1].color = normalColor; // for making the slot to the right normal
                } else if (currentEquipped == 0)
                {
                    Debug.Log("Reached the beginning");
                    BackgroundSlots[BackgroundSlots.Length - 1].color = normalColor; 
                } else if (currentEquipped == BackgroundSlots.Length-1)
                {
                    Debug.Log("Reached the beginning");
                    BackgroundSlots[0].color = normalColor;
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
            }
        }
    }

    private void Update()
    {
        // if the mouse wheel is greater than 0, we're scrolling up
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) 
        {
            if(currentEquipped+1 > HotBar.Length-1)
            {
                currentEquipped = 0;
            } else
                currentEquipped++;
            EquipHotbar();
        } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) // if less than 0, we're scrolling down.
        {
            if (currentEquipped - 1 < 0)
            {
                currentEquipped = HotBar.Length - 1;
            } else
                currentEquipped--;
            EquipHotbar();
        }
    }

    public void Start()
    {
//        Debug.Log("Hotbar Length: " + HotBar.Length);
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0"); // note that F0 eliminates decimals

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
}
