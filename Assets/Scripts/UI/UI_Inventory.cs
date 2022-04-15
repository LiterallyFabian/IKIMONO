using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlot;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    //Triggas av uppdateringar i inventory
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        //Tar bort alla itemslots f�rutom templaten
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlot) continue;
            Destroy(child.gameObject);
        }

        //G� igenom alla items i inventory och uppdatera UI
        float itemSlotSize = 125f;
        int x = 0;
        int y = 0;
        foreach (Item item in inventory.GetItemList())
        {
            //Skapa inventoryslot och placera ut i inventory
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSize, y * -itemSlotSize);

            //S�tt sprite till items sprite
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            //S�tt amountText till antalet items i inventory ifall de finns fler �n 1
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            if (item.GetAmount() > 1)
            {
                amountText.text = item.GetAmount().ToString();
            }
            else
            {
                amountText.text = "";
            }
            Debug.Log(item.GetAmount());

            //L�gg till referens till item i itemslot scriptet s� att det g�r att anv�nda fr�n UI
            itemSlotRectTransform.GetComponent<ItemSlot>().SetUp(item, inventory);

            //Uppdatera x och y koordinaterna s� att nya items hamnar r�tt i UI
            x++;
            if (x >= 3) { x = 0; y++; }
        }
    }

}
