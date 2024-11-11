using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImmediateDetection : MonoBehaviour
{
    public bool detected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("Player"))
        {
            Debug.Log(other.GameObject().name);
            // int test = AlarmEnemy();
            detected = true;
        }
    }

    // private int AlarmEnemy()
    // {
    //     return (int)EGuardState.Alarmed;
    // }
}
