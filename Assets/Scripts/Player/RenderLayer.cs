using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBackWall : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (player.transform.position.y < boxCollider.bounds.min.y)
        {
            spriteRenderer.sortingOrder = -2;
        }
        else
        {
            spriteRenderer.sortingOrder = +2;
        }
    }
}
