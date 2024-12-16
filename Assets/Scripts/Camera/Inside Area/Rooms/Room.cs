using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Room : MonoBehaviour
{
    public string roomName;
    public int roomId;
    [SerializeField] private string serverUrl = "http://localhost:8000";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Bounds triggerBounds = GetComponent<Collider2D>().bounds;

            Camera.main.GetComponent<InsideCamera>().UpdateCameraBounds(triggerBounds.min, triggerBounds.max);
            
            StartCoroutine(SendRoomDataToServer());
        }
    }

    IEnumerator SendRoomDataToServer()
    {
        // Create JSON data
        RoomData roomData = new RoomData { roomName = this.roomName, roomId = this.roomId };
        string jsonData = JsonUtility.ToJson(roomData);

        // Create a POST request
        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Room data sent successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Failed to send room data: " + request.error);
        }
    }

    [Serializable]
    public class RoomData
    {
        public string roomName;
        public int roomId;
    }
}