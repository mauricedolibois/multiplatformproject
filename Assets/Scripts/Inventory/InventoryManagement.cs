using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated = false;
    public ItemSlot[] itemSlot;
   
    void Start()
    {
        InventoryMenu.SetActive(menuActivated);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
        }
    }


    
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }
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
