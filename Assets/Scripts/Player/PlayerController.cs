using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    // Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    // movement
    public float speed;
    public float jumpForce;

    // groundcheck

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrectly, defaulting to " + speed.ToString());
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300f;
            Debug.Log("Jump Force was set incorrectly, defaulting to " + speed.ToString());
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrectly, defaulting to " + speed.ToString());
        }

        if (!groundCheck) 
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Roll")
            {
                anim.SetTrigger("roll");
            } 
            else if (curPlayingClip[0].clip.name == "Roll")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        /*
         
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("roll");
        }

 
        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;
        */

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("hInput", Mathf.Abs(hInput));
        
        if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX)
        {
            sr.flipX = !sr.flipX;
        }

        /*
        if (hInput != 0) 
        {
            sr.flipX = (hInput < 0);
        }
        */
    }
}
