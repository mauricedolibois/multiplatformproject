using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectApp : MonoBehaviour
{
    [SerializeField] public Button connectButton;   
    [SerializeField] public Text gameCodeText;  
    private RestAPI restAPI;                      

    void Start()
    {
        // Find the RestAPIManager in the scene and get the RestAPI script
        GameObject restAPIManager = GameObject.Find("RestAPIManager");
        if (restAPIManager != null)
        {
            restAPI = restAPIManager.GetComponent<RestAPI>();
        }
        else
        {
            Debug.LogError("RestAPIManager not found in the scene!");
        }

        // Check if a session exists
        StartCoroutine(CheckExistingSession());

        // Attach the OnClick listener to the button
        connectButton.onClick.AddListener(OnCreateSessionClicked);
    }

    // Called when the button is clicked
    void OnCreateSessionClicked()
    {
        if (restAPI != null)
        {
            StartCoroutine(CreateSessionWithUI());
        }
        else
        {
            Debug.LogError("RestAPI reference is null!");
        }
    }

    // Coroutine to check if an existing session is valid
    private IEnumerator CheckExistingSession()
    {
        string gameCode = PlayerPrefs.GetString("GameCode", "N/A");
        int sessionId = PlayerPrefs.GetInt("SessionID", -1);

        if (sessionId != -1 && gameCode != "N/A")
        {
            Debug.Log("Existing session found. Validating...");

            // Validate the session with the server
            bool sessionIsValid = false;
            yield return StartCoroutine(restAPI.ValidateSession(result => sessionIsValid = result));

            if (sessionIsValid)
            {
                connectButton.gameObject.SetActive(false);
                gameCodeText.text = "Game Code: \n" + gameCode;
                gameCodeText.gameObject.SetActive(true);

                Debug.Log("Existing session validated. Displaying Game Code.");
            }
            else
            {
                Debug.LogWarning("Session validation failed. Clearing stored session data.");
                PlayerPrefs.DeleteKey("GameCode");
                PlayerPrefs.DeleteKey("SessionID");
                connectButton.gameObject.SetActive(true);
                gameCodeText.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("No existing session found.");
            connectButton.gameObject.SetActive(true);
            gameCodeText.gameObject.SetActive(false);
        }
    }

    // Coroutine to create a new session and update the UI
    private IEnumerator CreateSessionWithUI()
    {
        yield return StartCoroutine(restAPI.PostCreateSession());

        // Retrieve the stored Game Code and Session ID from PlayerPrefs
        string gameCode = PlayerPrefs.GetString("GameCode", "N/A");
        int sessionId = PlayerPrefs.GetInt("SessionID", -1);

        if (sessionId != -1 && gameCode != "N/A")
        {
            connectButton.gameObject.SetActive(false);
            gameCodeText.text = "Game Code: \n" + gameCode;
            gameCodeText.gameObject.SetActive(true);

            Debug.Log("Game Code displayed successfully!");
        }
        else
        {
            Debug.LogError("Failed to retrieve session data from PlayerPrefs.");
        }
    }
}
