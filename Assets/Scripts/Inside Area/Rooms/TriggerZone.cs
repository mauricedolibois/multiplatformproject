using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public bool isTrigger1; 
    private RoomTrigger parentRoomTrigger;

    void Start()
    {
        parentRoomTrigger = GetComponentInParent<RoomTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentRoomTrigger.EnterRoomTrigger(isTrigger1);
        }
    }
}
