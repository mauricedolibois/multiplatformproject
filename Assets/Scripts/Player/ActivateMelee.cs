using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMelee : MonoBehaviour
{
    private GameObject melee;
    private float maxTime = 1f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        melee = transform.GetChild(1).gameObject;
        melee.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!melee.activeInHierarchy)
        {
            timer = 0f;
            MakeMeleeActive();
            return;
        }
        
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {   
            melee.SetActive(false);
        }
    }

    private void MakeMeleeActive()
    {
        if (Input.GetMouseButtonDown(0))
        {
            melee.SetActive(true);
        }
    }
}
