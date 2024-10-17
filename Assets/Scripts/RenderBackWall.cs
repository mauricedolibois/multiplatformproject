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
        if (player.transform.position.y > transform.position.y)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
