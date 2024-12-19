using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Continue()
    {
        GameObject.FindGameObjectWithTag("inv").GetComponent<InventoryManagement>().inventoryBlock = false;
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("inv"));
        Destroy(GameObject.FindGameObjectWithTag("SceneM"));
        SceneManager.LoadScene(0);
    }
}
