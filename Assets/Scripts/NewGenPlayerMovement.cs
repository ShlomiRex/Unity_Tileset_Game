using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenPlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 800f;
    public Transform groundCheck;


    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Tiles"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            //anim.SetBool("IsJumping", true);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("HorizontalSpeed", Mathf.Abs(h) * moveForce);

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        

        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        anim.SetBool("IsJumping", isGrounded);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool isGrounded = false;
    void OnCollision2DEnter(Collision2D collision)
    {
        /*
        if (collision.gameObject.layer == 8 //check the int value in layer manager(User Defined starts at 8) 
            && !isGrounded)
        {
            isGrounded = true;
        }
        */
        isGrounded = true;

        Debug.Log("Collision with " + collision.gameObject.name);
    }
    void OnCollision2DExit(Collision2D collision)
    {
        /*
        if (collision.gameObject.layer == 8 && isGrounded)
        {
            isGrounded = false;
        }
        */
        isGrounded = false;

        Debug.Log("Collision stopped with " + collision.gameObject.name);
    }
}
