using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Security.Cryptography;
using UnityEngine;

public class slim_move : Enemy
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    public Transform  leftpoint, rightpoint;
    public float Speed;
    private bool faceleft = true;
    //private Animator anim;
    private float leftx, rightx;

    protected override void Start()
    {
        base.Start();
        Coll = GetComponent<Collider2D>();
        rb = GetComponent <Rigidbody2D> ();
        //anim = GetComponent<Animator>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        //Destroy(leftpoint.gameObject);
        //Destroy(rightpoint.gameObject);
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        anim.SetFloat("move", 1);
        if (faceleft)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            if(transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
                anim.SetFloat("move", 0);
            }
        } 
        else 
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
                anim.SetFloat("move", 0);
            }
        }

    }

    
}
