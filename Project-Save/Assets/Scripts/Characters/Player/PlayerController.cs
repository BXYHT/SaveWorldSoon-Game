using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public Animator anim;
    public Collider2D coll;
    public LayerMask ground;
    public Transform groundCheck;
    public bool isGround, isJump;
    private PlayerStatus playerStatus;


    public int Gem = 0;
    public Text GemNum;



    private float dashCDTimer;
    private int dashCounter;
    public GameObject dashObj;
    private float StartDashTimer;


    private bool isClimbing;
    private bool isLadder;
    private float vClimb;

    bool jumpPressed;
    int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        playerStatus = GetComponent<PlayerStatus>();

        dashCounter = playerStatus.maxDashCount;
        dashCDTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        vClimb = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vClimb) > 0f)
        {
            anim.SetBool("climbing", true);

            isClimbing = true;
        }

        Dash();
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position , 0.1f , ground);
        Movement();
        Jump();
        SwitchAnim();
        ClimbLadder();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = true;
        }

        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Gem += 1;
            GemNum.text = Gem.ToString();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = false;
            isClimbing = false;
            anim.SetBool("climbing", false);

        }
    }

    void ClimbLadder()
    {
        if (isClimbing == true)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vClimb * speed);
        }
        else
        {
            rb.gravityScale = 2;
        }

    }


    void Movement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0) 
        {
            rb.velocity = new Vector2(hor * speed, rb.velocity.y);
            anim.SetFloat("running", 1);
        }  
        else  
        {
            if(isGround)
                rb.velocity = Vector2.zero;
            anim.SetFloat("running", 0);
        }
        if (hor!=0)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0&&isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()
    {
        if (isGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
        else if (!isGround && rb.velocity.y>0)
        {
            anim.SetBool("jumping",true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }


    void Dash()
    {
        if (dashCounter>0&&Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Go Dash
            dashCounter--;
            playerStatus.isDashing = true;

            // Initilize Timer
            dashCDTimer = playerStatus.dashCDTime;
            StartDashTimer = playerStatus.dashTime;

            if (playerStatus.increaseDamageAfterDash)
                playerStatus.damageFactor *= 2;
        }

        else if(playerStatus.isDashing)
        {
            StartDashTimer -= Time.deltaTime;

            if (StartDashTimer <= 0)
                playerStatus.isDashing = false;
            else
                anim.SetTrigger("Dash");
                rb.MovePosition(new Vector2(transform.position.x+transform.right.x * playerStatus.dashSpeed *Time.deltaTime,transform.position.y));
        }

        else if(dashCDTimer>0)// dashCD recover if not dashing
        {
            dashCDTimer -= Time.deltaTime;

            if(dashCDTimer<=0)
            {
                if (dashCounter + 1 <= playerStatus.maxDashCount)
                {
                    dashCounter++;
                    if (dashCounter < playerStatus.maxDashCount)
                        dashCDTimer = playerStatus.dashCDTime;
                }
            }
        }
    }

}
