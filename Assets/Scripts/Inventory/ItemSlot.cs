using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    
    //ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    
    //ITEM SLOT
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;
    
    //ITEM DESCRIPTION
    public Image itemDescriptionImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    
    
    public GameObject selectedShader;
    public bool thisItemSelected;
    
    private InventoryManagement inventoryManagement;

    private void Start()
    {
        inventoryManagement = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagement>();
    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;
        
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManagement.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }
    public void OnRightClick()
    {
        
    }
    
}
