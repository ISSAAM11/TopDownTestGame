using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public Image itemImage;
    public Text itemName;
    public Text itemPrice;
    public Text itemAbility;
    public Image itemRequirement1;
    public Image itemRequirement2;
    public Button buyButton;

    public Sprite requirementEmpty;

    public ItemsDB itemsDB;

    public MyItems MyItems;

    public void ItemSelected(int i) {
        ItemsClass item = itemsDB.getItem(i);

        itemImage.sprite = item.sprite;
        itemName.text = "Name: " + item.itemName;
        itemPrice.text = "Price: " + item.price;
        string abilityString = "";
        if (item.Health != 0)
            abilityString += "+" +item.Health.ToString() + " Health ";
        if (item.Damage != 0)
            abilityString += "+" + item.Damage.ToString() + " Damage ";
        if (item.speed != 0)
            abilityString += "+" + item.speed.ToString() + " Speed ";

        itemAbility.text = abilityString ;
        if (item.cryteria.Count >= 1)
            itemRequirement1.sprite = itemsDB.getItem(item.cryteria[0]).sprite;
        else
            itemRequirement1.sprite = requirementEmpty;
        if (item.cryteria.Count >= 2) 
            itemRequirement2.sprite = itemsDB.getItem(item.cryteria[1]).sprite;
        else 
            itemRequirement2.sprite = requirementEmpty;

        MyItems.selectedItemid = i;
    }
}