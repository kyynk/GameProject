using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class move : MonoBehaviour
{

    public GameObject AAAA;
    public AudioSource coinAudio;
    public AudioSource jumpAudio;
    public AudioSource runAudio;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    public float jumpForce, speed;
    public Transform groundCheck;
    public LayerMask ground;
    public float dieTime;
   

    [Header("CD的UI组件")]
    public Image cdImage;
    [Header("Dash參數")]
    public float dashTime;//dash時間
    private float dashTimeLeft;
    private float LastDash = -10f;
    public float dashCoolDown;
    public float dashSpeed;

    public bool isGround, isJump, isDashing;

    bool jumpPressed;
    int jumpCount;
    private float horizontalMove;



    public int Coin = 0;

    public Text coinNum;
    private bool isHurt;//默認是false

    public Transform keyFollowPoint;
    public key followingKey;

    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Restart", 0f);
        }
            if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(Time.time >= (LastDash + dashCoolDown))
            {
                //可以執行dash
                ReadyToDash();
            }
        }
        cdImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
        coinNum.text = Coin.ToString();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Dash();
        if (isDashing)
            return;
        Jump();
        GroundMovement();
        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
      
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
            runAudio.Play();
        }
    }
    
    void Jump()
    {
        if(isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAudio.Play();
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed && jumpCount >0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAudio.Play();
            jumpCount--;
            jumpPressed = false;
        }
    }
    
    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if(isGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
        else if(!isGround && rb.velocity.y >0)
        {
            anim.SetBool("jumping", true);
        }
        /*else if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }*/
        else if(rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
   

    //碰撞觸發氣
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞櫻桃
        if(collision.tag == "coin")
        {
              
            //Destroy(collision.gameObject);
            //coin += 1;
            collision.GetComponent<Animator>().Play("isGot");
            //coinAudio.Play();
            //coinNum.text = Coin.ToString();
        }
        if (collision.tag == "Traps")
        {           
            anim.SetTrigger("die");
            Invoke("Restart", 1f);
            //Invoke("KillPlayer", dieTime);

        }
        if (collision.tag == "deadline")
        {
            Invoke("Restart", 1f);
        }

        if(collision.tag == "enemy")
        {
            Invoke("Restart", 1f);
        }
        if (collision.tag == "vaccine")
        {
            Destroy(collision.gameObject);
        }

    }

    //消滅敵人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool( "falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else
            {
                anim.SetTrigger("die");
                Invoke("Restart", 1f);
                //Invoke("KillPlayer", dieTime);

            }
            
        }
    }

    void world()
    {
        SceneManager.LoadScene(4);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        LastDash = Time.time;
        cdImage.fillAmount = 1;
    }

    void Dash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {

                /*if (rb.velocity.y > 0 && !isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);//在空中Dash向上
                }*/
                rb.velocity = new Vector2(dashSpeed * horizontalMove, rb.velocity.y);//地面Dash

                dashTimeLeft -= Time.deltaTime;

                shadowpool.instance.GetFormPool();
            }
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                /*if (!isGround)
                {
                    //目的为了在空中结束 Dash 的时候可以接一个小跳跃。根据自己需要随意删减调整
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);
                }*/
            }
        }
    }


    public void CoinCount()
    {
        Coin += 1;
    }

    /*void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("attack");
        }
    }*/
}
