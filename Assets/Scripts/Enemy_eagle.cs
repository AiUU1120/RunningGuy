using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    public Transform uppoint, downpoint;
    public float Speed;
    private float upy, downy;
    private bool Facedown = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        upy = uppoint.position.y;
        downy = downpoint.position.y;
        Destroy(uppoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Facedown)
        {
            rb.velocity = new Vector2(0, -Speed);
            if (transform.position.y < downy)
            {
                Facedown = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, Speed);
            if (transform.position.y > upy)
            { 
                Facedown = true;
            }
        }
    }
    
}
