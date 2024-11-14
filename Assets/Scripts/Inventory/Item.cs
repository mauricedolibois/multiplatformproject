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
    
    //make sure they do not respawn after scene changing
    [SerializeField] private string itemId;
    private static HashSet<string> collectedItems = new HashSet<string>();
    
    void Start()
    {
        if (collectedItems.Contains(itemId))
        {
            Destroy(gameObject); 
            return;
        }
        
        inventoryManager = GameObject.Find("UICanvas").GetComponent<InventoryManagement>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

            if (leftOverItems <= 0)
            {
                collectedItems.Add(itemId);
                gameObject.SetActive(false); 
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}