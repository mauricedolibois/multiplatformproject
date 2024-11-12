using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private string itemname;
    [SerializeField] private TextMeshProUGUI lockedText; // Reference to the TextMeshProUGUI for "locked" message
    

    // Checks if the item is available in the inventory using the FindItem method
    public bool CheckItem()
    {
        if (!string.IsNullOrEmpty(itemname))
        {
            // Find the InventoryManagement component and check for the item
            InventoryManagement inv = GameObject.FindWithTag("inv").GetComponent<InventoryManagement>();
            return inv != null && inv.FindItem(itemname); // Use FindItem directly
        }
        return true; // Does not need item
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
            else
            {
                // If the item is not found, show "locked" text
                StartCoroutine(ShowLockedMessage());
            }
        }
    }

    // Coroutine to display "locked" message for 2 seconds
    private IEnumerator ShowLockedMessage()
    {
        if (lockedText != null)
        {
            lockedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2); // Wait for 2 seconds
            lockedText.gameObject.SetActive(false);
        }
    }
}
