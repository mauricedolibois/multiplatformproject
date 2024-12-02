using UnityEngine;

public class RiseToTarget : MonoBehaviour
{
    // Target Y position
    public float targetY = 5f;

    // Speed of rising
    public float riseSpeed = 2f;

    private void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Check if the object's Y position is below the target Y
        if (currentPosition.y < targetY)
        {
            // Calculate the new Y position
            float newY = Mathf.MoveTowards(currentPosition.y, targetY, riseSpeed * Time.deltaTime);

            // Update the object's position
            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}