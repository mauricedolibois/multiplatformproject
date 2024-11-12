using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExitWithKey : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private string itemname;

    // Checks if the item is available in the inventory using the FindItem method
    public bool CheckItem()
    {
        if (itemname != "")
        {
            // Find the InventoryManagement component and check for the item
            InventoryManagement inv = GameObject.FindWithTag("inv").GetComponent<InventoryManagement>();
            return inv.FindItem(itemname); // Use FindItem directly
        }
        return true; // doesnt need item
    }

// Trigger to load the scene if the player has the required item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())  
        {
            if (CheckItem()) 
            {
                
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
