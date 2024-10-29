using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [SerializeField] private string transitionName;

    private void Start()
    {
        if (SceneManagement.Instance.SceneTransitionName == transitionName);
        {
            PlayerMovement.Instance.transform.position = transform.position;
        }
    }
}
