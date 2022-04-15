using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item, int amount)
    {
        //Om item kan stacka -> Hitta i listan och addera p� amount
        //Finns item inte i listan, l�gg till
        if (item.IsStackable())
        {
            bool alreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.ItemScriptableObject == item.ItemScriptableObject)
                {
                    inventoryItem.AddAmount(amount);
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                item.AddAmount(amount);
                itemList.Add(item);
            }
        }
        else
        {
            item.AddAmount(amount);
            itemList.Add(item);
        }
        //Trigga f�r�ndring i UI
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item, int amount)
    {
        //Om item kan stacka -> Hitta i listan och substrahera p� amount
        //Fanns item i listan och �r nu under 1, ta bort ur listan
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.ItemScriptableObject == item.ItemScriptableObject)
                {
                    inventoryItem.RemoveAmount(amount);
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.GetAmount() <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
