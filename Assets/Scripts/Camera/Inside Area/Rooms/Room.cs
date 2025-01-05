using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class Room : MonoBehaviour
{
    public string roomName;
    private RestAPI restAPI;
    public int roomId;

    private TextMeshProUGUI areaText;

    void Start()
    {
        areaText = GameObject.FindGameObjectWithTag("Area").GetComponent<TextMeshProUGUI>();

        GameObject restAPIManager = GameObject.Find("RestAPIManager");
        if (restAPIManager != null)
        {
            restAPI = restAPIManager.GetComponent<RestAPI>();
        }
        else
        {
            Debug.LogError("RestAPIManager not found in the scene!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Bounds triggerBounds = GetComponent<Collider2D>().bounds;

            InsideCamera insideCamera = Camera.main.GetComponent<InsideCamera>();
            if (insideCamera != null)
            {
                insideCamera.UpdateCameraBounds(triggerBounds.min, triggerBounds.max);
            }
            

            if (roomId > 0)
            {
                areaText.text = roomName;

                StartCoroutine(restAPI.UpdateCurrentRoom(roomId, success =>
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
    }
}