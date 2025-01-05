using System.Collections;
using UnityEngine;

public class HandleSidemission : MonoBehaviour
{
    [SerializeField] private GameObject freePrisoners;
    [SerializeField] private GameObject sidemissionText;

    private RestAPI restAPI;
    private CanvasGroup sidemissionCanvasGroup;
    private bool isCheckingInput = true;

    void Start()
    {
        freePrisoners.SetActive(false);

        GameObject restAPIManager = GameObject.Find("RestAPIManager");
        if (restAPIManager != null)
        {
            restAPI = restAPIManager.GetComponent<RestAPI>();
        }
        else
        {
            Debug.LogError("RestAPIManager not found in the scene!");
        }

        // Get or add a CanvasGroup component to sidemissionText
        sidemissionCanvasGroup = sidemissionText.GetComponent<CanvasGroup>();
        if (sidemissionCanvasGroup == null)
        {
            sidemissionCanvasGroup = sidemissionText.AddComponent<CanvasGroup>();
        }

        sidemissionText.SetActive(false);

        // Start the periodic input check
        StartCoroutine(CheckInputPeriodically());
    }

    private IEnumerator CheckInputPeriodically()
    {
        while (isCheckingInput)
        {
            if (restAPI != null)
            {

                yield return StartCoroutine(restAPI.GetCurrentRoom(id =>
                {
                    if (id == 6)
                    {
                        // Call CheckInput method and handle the response
                        StartCoroutine(restAPI.CheckInput(result =>
                        {
                            if (result)
                            {
                                TriggerSideMission();
                                isCheckingInput = false;
                            }
                        }));
                    }
                }));
            }

            yield return new WaitForSeconds(2f); 
        }
    }

    private void TriggerSideMission()
    {
        freePrisoners.SetActive(true);
        if (PlayerPrefs.GetInt("SideMissionComplete") != 1)
        {
            sidemissionText.SetActive(true);
        }
        sidemissionCanvasGroup.alpha = 1f; 
        StartCoroutine(FadeOutGameObject());
        PlayerPrefs.SetInt("SideMissionComplete", 1);
    }

    private IEnumerator FadeOutGameObject()
    {
        yield return new WaitForSeconds(2f);

        float fadeDuration = 1f; 
        float startAlpha = sidemissionCanvasGroup.alpha;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            sidemissionCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, normalizedTime);
            yield return null;
        }

        // Ensure alpha is set to 0
        sidemissionCanvasGroup.alpha = 0;

        sidemissionText.SetActive(false); // hide the GameObject after fading out
    }
}
