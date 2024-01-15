using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsClass
{
    public int itemId;
    public string itemName;
    public int price;
    public Sprite sprite;

    public int Health;
    public int Damage;
    public float speed;

    public List<int> cryteria;

}
