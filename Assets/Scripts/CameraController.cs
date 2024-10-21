using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector2 buffer = new Vector2(0.5f, 0.3f);
    [SerializeField] private float zoomOutSize = 7.0f;
    [SerializeField] private float normalZoomSize = 5.0f;
    [SerializeField] private float zoomSpeed = 5.0f;

    private Vector3 velocity = Vector3.zero;
    private Transform playerTransform;
    private Camera cameraComponent;
    private PlayerMovement playerMovement; 
    private Coroutine currentZoomCoroutine = null; 

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.onShiftPressedEvent.AddListener(HandleShiftPressed);
            playerMovement.onShiftReleasedEvent.AddListener(HandleShiftReleased);
        }

        cameraComponent = GetComponent<Camera>();
        if (cameraComponent == null)
        {
            Debug.LogError("No Camera component found on this object.");
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + offset;
            if (Mathf.Abs(targetPosition.x - transform.position.x) > buffer.x ||
                Mathf.Abs(targetPosition.y - transform.position.y) > buffer.y)
            {
                //TODO: remove Camera stuttering
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }

    private void HandleShiftPressed()
    {
        if (currentZoomCoroutine != null)
        {
            StopCoroutine(currentZoomCoroutine);
        }
        currentZoomCoroutine = StartCoroutine(ZoomTo(zoomOutSize));
    }

    private void HandleShiftReleased()
    {
        if (currentZoomCoroutine != null)
        {
            StopCoroutine(currentZoomCoroutine);
        }
        currentZoomCoroutine = StartCoroutine(ZoomTo(normalZoomSize));
    }

    private System.Collections.IEnumerator ZoomTo(float targetSize)
    {
        Vector3 targetPosition = playerTransform.position + offset;

        while (Mathf.Abs(cameraComponent.orthographicSize - targetSize) > 0.01f && 
               Mathf.Abs(transform.position.x - targetPosition.x) > 0.00f &&
               Mathf.Abs(transform.position.y - targetPosition.y) > 0.00f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime* 0.5f);
            cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        cameraComponent.orthographicSize = targetSize;
        currentZoomCoroutine = null;
    }
}
