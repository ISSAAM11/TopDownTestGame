using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemsStats", menuName = "ItemsDB")]
public class ItemsDB : ScriptableObject
{
    [SerializeField] ItemsClass[] items;

    public ItemsClass getItem(int i)
    {
        return items[i];
    }
}
