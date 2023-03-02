using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public Animator anim;

    public float speed, jumpForce;
    private float horizontalMove;
    public Transform GroundCheck;
    public Transform FirePoint;
    public LayerMask ground;
    private Text HpNumberText;


    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;
    public int HP = 100;
    private float timer = 0;
    private float face = 1;
    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        HpNumberText = GameObject.Find("HpNumber").GetComponent<Text>();//获取ui对于的text

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        timer += Time.deltaTime;
        if (Input.GetButtonDown("Attack") && timer >0.5f)
        {
            timer = 0;
            anim.SetTrigger("Shooting");
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);

        if (!isHurt)
        {

            GroundMovement();

            Jump();
        }
        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
       
        if (horizontalMove != 0 && face != horizontalMove)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
            face = (face == 1) ? -1 : 1;
            FirePoint.Rotate(0f, 180f, 0f);
        }

    }


    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-7, rb.velocity.y);//弹开
                isHurt = true;
                HP -= 25;
                HpNumberText.text = HP.ToString();
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(7, rb.velocity.y);
                isHurt = true;
                HP -= 25;
                HpNumberText.text = HP.ToString();
            }
        }
        /*if(health <=0)
        {
            Destroy(gameObject);
        }*/
    }


    void SwitchAnim()//动画切换
    {
        anim.SetFloat("Running", Mathf.Abs(rb.velocity.x));
         if (isHurt)
        {
            anim.SetBool("Hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 3f)
            {
                anim.SetBool("Hurt", false);
                anim.SetBool("Idle", true);
                isHurt = false;
            }
        }

        if (isGround)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("Jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
       
    }
}