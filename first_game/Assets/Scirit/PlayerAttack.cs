using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    private Animator anim;
    public float time;
    public float StartTime;
    private PolygonCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {

        if (Input.GetButtonDown("Attack"))
        {
            coll.enabled = true;
            anim.SetTrigger("attack");
            StartCoroutine(StartAttack());
        }

    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);
        coll.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<Animator>().Play("die");
        }
    }

    
}
