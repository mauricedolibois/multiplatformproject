using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSidemissionComplete : MonoBehaviour
{
    [SerializeField] private GameObject sidemissionCompleteUI;
    void Start()
    {
        sidemissionCompleteUI.SetActive(false);
        if (PlayerPrefs.GetInt("SideMissionComplete") == 1)
        {
            sidemissionCompleteUI.SetActive(true);
        }
    
    }


}
