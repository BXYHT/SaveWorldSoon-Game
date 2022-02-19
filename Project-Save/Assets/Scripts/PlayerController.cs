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


    public int Gem = 0;
    public Text GemNum;


    public float dashSpeed;
    public float dashTime;
    public GameObject dashObj;
    private float StartDashTimer;
    private bool isDashing = false;

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
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position , 0.1f , ground);
        movement();
        jump();
        switchAnim();
        climbLadder();
        Dash();
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

    void climbLadder()
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


    void movement()
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

    void jump()
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

    void switchAnim()
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
        if (!isDashing)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //dashObj.SetActive(true);
                isDashing = true;
                StartDashTimer = dashTime;
            }

        }
        else
        {
            StartDashTimer -= Time.deltaTime;
            if(StartDashTimer <= 0)
            {
                isDashing = false;
                //dashObj.SetActive(false);
            }
            else
            {
                rb.velocity = transform.right * dashSpeed;
            }
        }
    }

}
