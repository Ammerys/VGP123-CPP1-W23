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
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("hInput", Mathf.Abs(hInput));
    }
}
