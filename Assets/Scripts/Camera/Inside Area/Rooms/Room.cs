using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Room : MonoBehaviour
{
    public string roomName;
    public int roomId;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Bounds triggerBounds = GetComponent<Collider2D>().bounds;

            Camera.main.GetComponent<InsideCamera>().UpdateCameraBounds(triggerBounds.min, triggerBounds.max);

            //RestAPI.postCurrentRoom(roomId);
        }
    }

 

    [Serializable]
    public class RoomData
    {
        public string roomName;
        public int roomId;
    }
}