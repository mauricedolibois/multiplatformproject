using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void Newgame()
   {
      SceneManager.LoadScene(1);
      //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(180, 1.38f, 175);
   }

   public void Quitgame()
   {
      Application.Quit();
   }
}

