using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.Instance.SetMovementAllowed(false);
            StartCoroutine(WaitAndLoadScene(2f));
        }
    }
    private IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
        Destroy(GameObject.FindGameObjectWithTag("inv"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Time.timeScale = 1;
    }
}