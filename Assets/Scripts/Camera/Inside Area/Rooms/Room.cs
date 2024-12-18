using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Room : MonoBehaviour
{
    public string roomName;
    public RestAPI restAPI;
    public int roomId;

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Bounds triggerBounds = GetComponent<Collider2D>().bounds;

            Camera.main.GetComponent<InsideCamera>().UpdateCameraBounds(triggerBounds.min, triggerBounds.max);

            if (roomId!=0)
            {
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

 

    [Serializable]
    public class RoomData
    {
        public string roomName;
        public int roomId;
    }
}