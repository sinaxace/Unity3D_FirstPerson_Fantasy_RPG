using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code typed from this tutorial: https://youtu.be/oRPNWBvfsQk?list=PLE70FML1U9svBs9GBdhvesMkd0GuprFho

public class PlayerData : MonoBehaviour
{
    // for the RPG, we'll need a variable to store which quest the player is on atm.
    // quest number 0 is the default and it means that they're not on an actual quest.
    public int questNumber;
    public float dialogueNumber;
}
