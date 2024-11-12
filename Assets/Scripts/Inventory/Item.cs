using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    
    [SerializeField]
    private int quantity;

    [SerializeField] 
    private Sprite sprite;
    
    [TextArea]
    [SerializeField] 
    private string itemDescription;
    
    private InventoryManagement inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("UICanvas").GetComponent<InventoryManagement>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
                Destroy(gameObject);
            else 
                quantity = leftOverItems;
        }
    }
}
