using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RestAPI : MonoBehaviour
{
    private const string apiUrl = "http://localhost:3000";

    // only for testing
    public void CreateSession()
    {
        StartCoroutine(PostCreateSession());
    }

    // Coroutine to send a POST request to create a session
    //store the session data in PlayerPrefs
    //Can be accessed by this: PlayerPrefs.GetInt("SessionID"), PlayerPrefs.GetString("GameCode")
    public IEnumerator PostCreateSession()
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl+"/game/createSession", ""))
        {
            request.SetRequestHeader("Content-Type", "application/json"); // Optional: for clarity
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                SessionModel sessionData = JsonUtility.FromJson<SessionModel>(jsonResponse);

                if (sessionData != null)
                {
                    // Store session details in PlayerPrefs
                    PlayerPrefs.SetString("GameCode", sessionData.code);
                    PlayerPrefs.SetInt("SessionID", sessionData.sessionId);
                   
                }
                else
                {
                    Debug.LogError("Failed to parse session data.");
                }
            }
        }
    }

   public IEnumerator ValidateSession(System.Action<bool> callback)
{
    // Get GameCode and SessionID from PlayerPrefs
    string storedGameCode = PlayerPrefs.GetString("GameCode", "N/A");
    int storedSessionId = PlayerPrefs.GetInt("SessionID", -1);

    if (storedGameCode == "N/A" || storedSessionId == -1)
    {
        Debug.LogError("No GameCode or SessionID found in PlayerPrefs.");
        callback(false);
        yield break;
    }

    string url = $"{apiUrl}/game/checkCode/{storedGameCode}";

    using (UnityWebRequest request = UnityWebRequest.Get(url))
    {
        request.SetRequestHeader("Content-Type", "application/json"); 
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            callback(false);
        }
        else
        {
            string response = request.downloadHandler.text;

            int returnedSessionId;
            if (int.TryParse(response, out returnedSessionId))
            {
                // Compare returned Session ID with the stored Session ID
                if (returnedSessionId == storedSessionId)
                {
                    callback(true);
                }
                else
                {
                    callback(false);
                }
            }
            else
            {
                callback(false);
            }
        }
    }
}

}
