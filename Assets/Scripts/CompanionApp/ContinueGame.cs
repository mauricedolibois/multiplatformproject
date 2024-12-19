using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    
    [SerializeField] public Button continueButton;
    private RestAPI restAPI;
    private int outsideId = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject restAPIManager = GameObject.Find("RestAPIManager");
        if (restAPIManager != null)
        {
            restAPI = restAPIManager.GetComponent<RestAPI>();
        }
        else
        {
            Debug.LogError("RestAPIManager not found in the scene!");
        }

        continueButton.onClick.AddListener(ContinueGameInput);
    }

    private void ContinueGameInput()
    {
        if (restAPI != null)
        {
            StartCoroutine(CallEndpoint());
        }
        else
        {
            Debug.LogError("RestAPI reference is null!");
        }
    }

    private IEnumerator CallEndpoint()
    {
        yield return StartCoroutine(restAPI.UpdateCurrentRoom(outsideId, success =>
        {
            if (success)
            {
                Debug.Log("Room updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update the room.");
            }
        }));
    }
}