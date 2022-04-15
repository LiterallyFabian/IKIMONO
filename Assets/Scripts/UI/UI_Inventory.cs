using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory _inventory;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private Transform _itemSlot;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    // Triggas av uppdateringar i inventory.
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        // Tar bort alla itemslots f�rutom templaten.
        foreach (Transform child in _itemSlotContainer)
        {
            if (child == _itemSlot) continue;
            Destroy(child.gameObject);
        }

        // G� igenom alla items i inventory och uppdatera UI.
        float _itemSlotSize = 125f;
        int _x = 0;
        int _y = 0;
        foreach (Item item in _inventory.GetItemList())
        {
            // Skapa inventoryslot och placera ut i inventory.
            RectTransform _itemSlotRectTransform = Instantiate(_itemSlot, _itemSlotContainer).GetComponent<RectTransform>();
            _itemSlotRectTransform.gameObject.SetActive(true);
            _itemSlotRectTransform.anchoredPosition = new Vector2(_x * _itemSlotSize, _y * -_itemSlotSize);

            // S�tt sprite till items sprite.
            Image _image = _itemSlotRectTransform.Find("Image").GetComponent<Image>();
            _image.sprite = item.GetSprite();

            // S�tt amountText till antalet items i inventory ifall de finns fler �n 1.
            Text _amountText = _itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            if (item.GetAmount() > 1)
            {
                _amountText.text = item.GetAmount().ToString();
            }
            else
            {
                _amountText.text = "";
            }

            // L�gg till referens till item i itemslot scriptet s� att det g�r att anv�nda fr�n UI.
            _itemSlotRectTransform.GetComponent<ItemSlot>().SetUp(item, _inventory);

            // Uppdatera x och y koordinaterna s� att nya items hamnar r�tt i UI.
            _x++;
            if (_x >= 3) { _x = 0; _y++; }
        }
    }

}
