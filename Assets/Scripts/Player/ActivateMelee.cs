using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMelee : MonoBehaviour
{
    private GameObject melee;
    [SerializeField] GameObject directionIndicator;
    private float maxTime = 0.5f;
    private float timer = 0f;
    private PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        
        melee = transform.GetChild(1).gameObject;
        melee.SetActive(false);
        directionIndicator.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!melee.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MakeMeleeActive();
            }
        }
        else if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {   
            directionIndicator.GetComponent<SpriteRenderer>().color = Color.white;
            melee.SetActive(false);
            playerMovement.MeleeSpeed(melee.activeInHierarchy);
        }
    }

    private void MakeMeleeActive()
    {
        //if (Input.GetMouseButtonDown(0))
            timer = 0f;
            melee.SetActive(true);
            directionIndicator.GetComponent<SpriteRenderer>().color = Color.red;
            playerMovement.MeleeSpeed(melee.activeInHierarchy);
    }
}
