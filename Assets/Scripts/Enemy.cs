using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    private Collider2D coll;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        coll.enabled = false;
        anim.SetTrigger("death");
        deathAudio.Play();
    }
}
