using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardText : MonoBehaviour
{

    public int roomID;
    private RestAPI restAPI;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject boardText;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject restAPIManager = GameObject.Find("RestAPIManager");
        if (restAPIManager != null)
        {
            restAPI = restAPIManager.GetComponent<RestAPI>();
        }
        else
        {
            Debug.LogError("RestAPIManager not found in the scene!");
        }

        if (roomID != 0){
        StartCoroutine(restAPI.GetFrequency(roomID, frequency =>
        {
            if (frequency != null)
            {
                Debug.Log("Frequency: " + frequency);
                boardText.GetComponent<Text>().text = frequency+"mHz";
            }
            else
            {
                Debug.LogError("Failed to get frequency.");
                spriteRenderer.gameObject.SetActive(false);
            }
        }));
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
