using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : Singleton<InventoryManagement>

{
    public GameObject InventoryMenu;
    private bool menuActivated = false;
    public ItemSlot[] itemSlot;
    public bool inventoryBlock = false;
    
    void Start()
    {
        InventoryMenu.SetActive(menuActivated);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryBlock == false)
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            
            PlayerMovement.Instance.SetMovementAllowed(!menuActivated); 
        }
    }

    public bool FindItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName.Equals(itemName) && itemSlot[i].quantity > 0)
            {
                return true;
            }
        }
        return false;
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, quantity, itemSprite, itemDescription);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    
}
