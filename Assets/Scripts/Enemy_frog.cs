using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public float Speed;
    public float JumpForce;
    private float leftx, rightx;
    private bool Faceleft = true;
    //private Animator anim;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    { 
        if(Faceleft)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < leftx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(Speed, JumpForce);               
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if(anim.GetBool("jump"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jump", false);
                anim.SetBool("fall", true);
            }
        }
        else if(coll.IsTouchingLayers(Ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);
        }
    }
}
