using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MyItems : MonoBehaviour
{
    public List<ItemsClass> myItems = new List<ItemsClass>();
    public ItemsDB itemsDB ;
    public Transform ItemsTransform;

    public PlayerStats playerStats;
    public PlayerControl playerControl;

    public int selectedItemid = 1 ;
    public void buyItem () 
    {
        ItemsClass item = itemsDB.getItem(selectedItemid);
        if (playerStats.CoinsAmount >= item.price)
        {
            if (item.cryteria.Count == 0 && myItems.Count < 3)
            {
                playerStats.CoinsAmount -= item.price;
                myItems.Add(item);
                ItemsTransform.GetChild(myItems.Count - 1).gameObject.GetComponent<Image>().sprite = item.sprite;
                playerStats.updateStats();          // damage and HP
                playerControl.updateStats(myItems); // speed
            }
            else
            {
                checkItemRequiritemement(myItems, item.cryteria, item);
            }
        }
    }

    bool checkItemRequiritemement(List<ItemsClass> myItems, List<int> criteria, ItemsClass item)
    {

        bool allItemsExist = criteria.All(itemId => myItems.Any(item => item.itemId == itemId));
        if (allItemsExist)
        {
            myItems.RemoveAll(item => criteria.Contains(item.itemId));
            playerStats.CoinsAmount -= item.price;
            myItems.Add(item);

            for (int i = 0; i < ItemsTransform.childCount; i++)
            {
                ItemsTransform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            }

            int index = 0;
            foreach (ItemsClass thisItem in myItems)
            {
                ItemsTransform.GetChild(index).gameObject.GetComponent<Image>().sprite = thisItem.sprite;
                index++;
            }

            playerStats.updateStats();          // damage and HP
            playerControl.updateStats(myItems); // speed

        }
        return allItemsExist;
    }
}
