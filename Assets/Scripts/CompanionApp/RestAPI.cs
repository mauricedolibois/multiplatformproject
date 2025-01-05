using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RestAPI : MonoBehaviour
{

    //private const string apiUrl = "http://localhost:3000";	
    private const string apiUrl = "https://operationsilentchaos.vercel.app";

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
// Function to get the current room
    public IEnumerator GetCurrentRoom(System.Action<int> callback)
    {
        int sessionId = PlayerPrefs.GetInt("SessionID", -1);

        if (sessionId == -1)
        {
            Debug.LogError("No SessionID found in PlayerPrefs.");
            callback(0);
            yield break;
        }
        
        string url = $"{apiUrl}/room/getCurrentRoom/{sessionId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 10; // Timeout set to 10 seconds

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Request failed with error: {request.error}");
                callback(0);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;

                RoomModel roomData = JsonUtility.FromJson<RoomModel>(jsonResponse);

                
                if (roomData != null)
                {
                    callback(roomData.roomId);
                }
            }
        }
    }
   
// Function to update the current room
    public IEnumerator UpdateCurrentRoom(int roomId, System.Action<bool> callback)
    {
        int sessionId = PlayerPrefs.GetInt("SessionID", -1);

        if (sessionId == -1)
        {
            Debug.LogError("No SessionID found in PlayerPrefs.");
            callback(false);
            yield break;
        }

        string url = $"{apiUrl}/room/changeCurrentRoom/{sessionId}/{roomId}";

        using (UnityWebRequest request = UnityWebRequest.Put(url, ""))
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
                callback(true);
            }
        }
    }


    // Function to get frequency for a specific room
   public IEnumerator GetFrequency(int roomId, System.Action<string> callback)
{
    int sessionId = PlayerPrefs.GetInt("SessionID", -1);

    if (sessionId == -1)
    {
        Debug.LogError("No SessionID found in PlayerPrefs.");
        callback(null);
        yield break;
    }

    string url = $"{apiUrl}/frequency/getFrequency/{sessionId}/{roomId}";

    using (UnityWebRequest request = UnityWebRequest.Get(url))
    {
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 10; // Timeout set to 10 seconds

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Request failed with error: {request.error}");
            callback(null);
        }
        else
        {
            string frequency = request.downloadHandler.text;
            callback(frequency);
        }
    }
}

public IEnumerator CheckInput(System.Action<bool> callback)
{
    int sessionId = PlayerPrefs.GetInt("SessionID", -1);

    if (sessionId == -1)
    {
        Debug.LogError("Invalid SessionID provided.");
        callback(false);
        yield break;
    }

    string url = $"{apiUrl}/input/checkInput/{sessionId}";

    using (UnityWebRequest request = UnityWebRequest.Get(url))
    {
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 10; // Timeout set to 10 seconds

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Request failed with error: {request.error}");
            callback(false);
        }
        else
        {
            string responseText = request.downloadHandler.text;

            bool result = responseText == "1";
            callback(result);
        }
    }
}

public IEnumerator ResetGame() 
    {
        // Get SessionID from PlayerPrefs
        int storedSessionId = PlayerPrefs.GetInt("SessionID", -1);

        if (storedSessionId == -1)
        {
            Debug.LogError("No SessionID found in PlayerPrefs.");
            yield break;
        }

        string url = $"{apiUrl}/game/resetCurrentSession/{storedSessionId}";
        
        using (UnityWebRequest request = UnityWebRequest.Put(url, ""))
        {
            request.SetRequestHeader("Content-Type", "application/json"); // Optional: for clarity
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("Reset all Inputs");
            }
        }
    }

}
