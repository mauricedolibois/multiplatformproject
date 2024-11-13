using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;
    [TextArea] [SerializeField] private string itemDescription;

    private InventoryManagement inventoryManager;

    // ID to track if the item was already picked up
    [SerializeField] private string uniqueItemID;

    void Start()
    {
        inventoryManager = GameObject.Find("UICanvas").GetComponent<InventoryManagement>();

        // Check if the item was already picked up
        if (PlayerPrefs.GetInt(uniqueItemID, 0) == 1)
        {
            Destroy(gameObject); // Destroy item if it was already picked up
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                // Mark item as picked up 
                PlayerPrefs.SetInt(uniqueItemID, 1);
                PlayerPrefs.Save(); 
                
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}