using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void Newgame()
    {
        PlayerPrefs.DeleteKey("SideMissionComplete");
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(UpdatePlayerPosition());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator UpdatePlayerPosition()
    {
        yield return null; // Wait one frame
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Player not found even after waiting.");
        }
    }


    public void Quitgame()
    {
        Application.Quit();
    }
}
