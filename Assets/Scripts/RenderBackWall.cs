using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBackWall : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Compare the Y position of the player and the object
        if (player.transform.position.y > transform.position.y)
        {
            // Render in front of the player
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            // Render behind the player
            spriteRenderer.sortingOrder = 1;
        }
    }
}
