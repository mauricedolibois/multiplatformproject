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

        // Cheat handling
        HandleCheats();
    }

    public bool FindItem(string itemName)
    {
        foreach (var slot in itemSlot)
        {
            if (slot.itemName.Equals(itemName) && slot.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        foreach (var slot in itemSlot)
        {
            if (!slot.isFull && slot.itemName == itemName || slot.quantity == 0)
            {
                int leftOverItems = slot.AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlot)
        {
            slot.selectedShader.SetActive(false);
            slot.thisItemSelected = false;
        }
    }

    private void HandleCheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddCheatItem("Laboratory Key", 1, "A key for the laboratory.\nDirty, left and forgotten by some guard.\n.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddCheatItem("Inner Laboratory Key", 1, "Once proud, this key was crucial to the lab’s second sector, until keycards replaced it. Now it lies forgotten on a balcony, dreaming of doors that no longer need it.\n\n");
        }
    }

    private void AddCheatItem(string itemName, int quantity, string description)
    {
        if (!FindItem(itemName))
        {
            Sprite placeholderSprite = null; 
            AddItem(itemName, quantity, placeholderSprite, description);
            Debug.Log($"{itemName} added via cheat.");
        }
    }
}
