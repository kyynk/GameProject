﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected AudioSource deathAudio;
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        deathAudio.Play();
        anim.SetTrigger("death");
    }

}
