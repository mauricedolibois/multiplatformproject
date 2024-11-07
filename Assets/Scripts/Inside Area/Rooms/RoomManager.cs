using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room startingRoom; 
    private Room currentRoom;
    private InsideCamera insideCamera;

    void Start()
    {
        if (startingRoom != null){
        insideCamera = Camera.main.GetComponent<InsideCamera>();
        EnterRoom(startingRoom);
        }
    }

    public void EnterRoom(Room newRoom)
    {
        if (newRoom != currentRoom)
        {
            currentRoom = newRoom;
            insideCamera.UpdateCameraBounds(newRoom.LowerLeft.position, newRoom.UpperRight.position);
        }
    }
}
