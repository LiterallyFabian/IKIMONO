using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item, int amount)
    {
        //Om item kan stacka -> Hitta i listan och addera p� amount
        //Finns item inte i listan, l�gg till
        if (item.IsStackable())
        {
            bool _alreadyInInventory = false;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.ItemScriptableObject == item.ItemScriptableObject)
                {
                    inventoryItem.AddAmount(amount);
                    _alreadyInInventory = true;
                }
            }
            if (!_alreadyInInventory)
            {
                item.AddAmount(amount);
                _itemList.Add(item);
            }
        }
        else
        {
            item.AddAmount(amount);
            _itemList.Add(item);
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
            Item _itemInInventory = null;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.ItemScriptableObject == item.ItemScriptableObject)
                {
                    inventoryItem.RemoveAmount(amount);
                    _itemInInventory = inventoryItem;
                }
            }
            if (_itemInInventory != null && _itemInInventory.GetAmount() <= 0)
            {
                _itemList.Remove(item);
            }
        }
        else
        {
            _itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}
