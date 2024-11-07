using UnityEngine;

public class InsideCamera : MonoBehaviour
{

    public float zoomFactor= 1.2f;
    public float transitionSpeed = 2f;
    private Vector3 roomPosition; 
    private float roomSize;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void UpdateCameraBounds(Vector3 lowerLeft, Vector3 upperRight)
    {
        // RoomCenter
        roomPosition = new Vector3(
            (lowerLeft.x + upperRight.x) / 2,
            (lowerLeft.y + upperRight.y) / 2,
            transform.position.z
        );

        float roomWidth = upperRight.x - lowerLeft.x;
        float roomHeight = upperRight.y - lowerLeft.y;

        roomSize = Mathf.Max(roomHeight / 2f, roomWidth / (2f *zoomFactor));
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, roomPosition, Time.deltaTime * transitionSpeed);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, roomSize, Time.deltaTime * transitionSpeed);
    }
}
