using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu :MonoBehaviour
{
    public GameObject PauseMenuu;
    private bool menuActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuu.SetActive(menuActivated);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.))
        {
            menuActivated = !menuActivated;
            PauseMenuu.SetActive(menuActivated);
        }

        //freeze
        if (menuActivated)
        {
            Time.timeScale = 0;
        }
        else if (!menuActivated)
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        menuActivated = !menuActivated;
        PauseMenuu.SetActive(menuActivated);
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("inv"));
        Destroy(GameObject.FindGameObjectWithTag("SceneM"));
        Item.ResetCollectedItems();
    }
}
