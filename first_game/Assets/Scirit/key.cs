using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    private bool isFollowing;

    public float followSpeed;

    public Transform followTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isFollowing)
            {
                move thePlayer = FindObjectOfType<move>();

                followTarget = thePlayer.keyFollowPoint;

                isFollowing = true;
                thePlayer.followingKey = this;
            }
        }
    }
}
