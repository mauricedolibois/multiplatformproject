using System.Collections;
using UnityEngine;

public class HandleSidemisson : MonoBehaviour
{
    [SerializeField] private GameObject freePrisoners;
    [SerializeField] private GameObject sidemissionText;

    private CanvasGroup sidemissionCanvasGroup;

    void Start()
    {
        freePrisoners.SetActive(false);

        // Get or add a CanvasGroup component to sidemissionText
        sidemissionCanvasGroup = sidemissionText.GetComponent<CanvasGroup>();
        if (sidemissionCanvasGroup == null)
        {
            sidemissionCanvasGroup = sidemissionText.AddComponent<CanvasGroup>();
        }

        sidemissionText.SetActive(false);
    }

    void Update()
    {
        // If key F is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            freePrisoners.SetActive(true);
            sidemissionText.SetActive(true);
            sidemissionCanvasGroup.alpha = 1f; // Ensure full visibility
            StartCoroutine(FadeOutGameObject());
        }
    }

    private IEnumerator FadeOutGameObject()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Gradually fade out the CanvasGroup's alpha
        float fadeDuration = 1f; // Duration of the fade-out
        float startAlpha = sidemissionCanvasGroup.alpha;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            sidemissionCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, normalizedTime);
            yield return null;
        }

        // Ensure alpha is set to 0
        sidemissionCanvasGroup.alpha = 0;

        sidemissionText.SetActive(false); // Hide the GameObject after fading out
    }
}
