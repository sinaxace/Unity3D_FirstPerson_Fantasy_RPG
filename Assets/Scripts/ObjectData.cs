using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for the wooden sword object
[CreateAssetMenu(menuName ="Object/type")]
public class ObjectData : ScriptableObject
{
    public Sprite sprite;
    public GameObject weaponPrefab;
}
