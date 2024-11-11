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
    public string itemDescription;
    
    [SerializeField]
    private int maxNumberOfItems;
    
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
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if(isFull)
            return quantity;
        
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        itemImage.sprite = itemSprite;
        
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
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
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
    }
    public void OnRightClick()
    {
        
    }
    
}
