using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    /*private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("pick", 0);
       
    }*/

    public void Death()
    {
        FindObjectOfType<move>().CoinCount();
        Destroy(gameObject);
    }
}
