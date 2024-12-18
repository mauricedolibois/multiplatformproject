using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    [SerializeField] public Button newGameButton;
    private RestAPI restAPI;
    
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

        newGameButton.onClick.AddListener(ResetInputTable);
    }

    void ResetInputTable()
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
        yield return StartCoroutine(restAPI.ResetGame());
    }
}
