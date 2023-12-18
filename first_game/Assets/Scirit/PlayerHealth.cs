using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;
    int trapsLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        trapsLayer = LayerMask.NameToLayer("Traps");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == trapsLayer)
        {
         
            gameObject.SetActive(false);
        }
    }
}
