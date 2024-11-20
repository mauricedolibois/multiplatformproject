using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName;
    public int roomId;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the bounds of the trigger
            Bounds triggerBounds = GetComponent<Collider2D>().bounds;

            // Call UpdateCameraBounds with the bounds' min and max values
            Camera.main.GetComponent<InsideCamera>().UpdateCameraBounds(triggerBounds.min, triggerBounds.max);
        }
    }
}
