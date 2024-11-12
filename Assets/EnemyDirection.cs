using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookInDirection();
    }

    private void LookInDirection()
    {
        float hVelocity = rb2d.velocity.x;
        Debug.Log(hVelocity);
        if (hVelocity < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
