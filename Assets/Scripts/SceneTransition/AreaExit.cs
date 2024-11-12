using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);  
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}