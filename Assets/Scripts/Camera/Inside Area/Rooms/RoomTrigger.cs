using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Room roomA; 
    public Room roomB; 
    private RoomManager roomManager;

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    public void EnterRoomTrigger(bool isTrigger1)
    {
        if (isTrigger1)
        {
            roomManager.EnterRoom(roomA);
        }
        else
        {
            roomManager.EnterRoom(roomB);
        }
    }
}
