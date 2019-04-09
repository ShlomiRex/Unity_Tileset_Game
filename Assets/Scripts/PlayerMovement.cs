using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float runSpeed = 1000f;
    [SerializeField]
    public float jumpForce = 600f;



    private float horizontalSpeed = 0f;
    private bool jump = false, isCrouching = false, facingLeft = false, pressingLeft = false, midAir = false;

    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        //rb.AddForce(new Vector2(runSpeed * Time.deltaTime, 0));
        horizontalSpeed = Input.GetAxis("Horizontal") * runSpeed;

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            midAir = true;
            isCrouching = false;
            animator.SetBool("IsJumping", true);
            rb.AddForce(new Vector2(0, jumpForce));
        }
        

        //Crouch
        if (Input.GetButtonDown("Crouch") && midAir == false)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", isCrouching);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", isCrouching);
        }

        animator.SetFloat("speed", Mathf.Abs(horizontalSpeed));

        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-runSpeed * Time.deltaTime, 0));
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(runSpeed * Time.deltaTime, 0));
        }

        jump = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        jump = false;
        midAir = false;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingLeft = !facingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
