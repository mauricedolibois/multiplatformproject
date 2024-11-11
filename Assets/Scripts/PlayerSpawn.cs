using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [SerializeField] private string transitionName;
    [SerializeField] private Vector3 playerScale;

    private void Start()
    {
        if (SceneManagement.Instance.SceneTransitionName == transitionName);
        {
            PlayerMovement.Instance.transform.position = transform.position;
        }
        PlayerMovement.Instance.transform.localScale = playerScale;
    }
}
