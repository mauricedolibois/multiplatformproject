using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float parallaxFactor = 0.05f; // Adjust this to control the intensity of the parallax effect.

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the image.
        initialPosition = transform.position;
    }

    void Update()
    {
        // Get the mouse position in screen space.
        Vector3 mousePosition = Input.mousePosition;

        // Calculate the center of the screen.
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Normalize the mouse position to a -1 to 1 range.
        Vector3 offset = new Vector3(
            (mousePosition.x - screenCenter.x) / screenCenter.x,
            (mousePosition.y - screenCenter.y) / screenCenter.y,
            0f
        );

        // Apply the parallax effect to the position.
        Vector3 parallaxPosition = initialPosition + offset * parallaxFactor;

        // Update the position of the object.
        transform.position = parallaxPosition;
    }
}