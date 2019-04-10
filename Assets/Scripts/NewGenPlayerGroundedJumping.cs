using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenPlayerGroundedJumping : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb2d;



    [SerializeField] public bool canDoubleJump = false; //TODO: Complete code
    [SerializeField] public float jumpForce = 800f;
    [SerializeField] public float second_jumpForce = 400f;

    private JumpStage jumpStage = JumpStage.noJumping; //This is reserved for canDoubleJump mechanisem
    private bool isGrounded = false;

    private enum JumpStage
    {
        noJumping,
        firstJump,
        secondJump
    }

    private bool jump = false; //Works great! Now each tile collision won't trigger jump (if not pressed jump)

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Debug.Log("CanDoubleJump = " + canDoubleJump);
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(canDoubleJump == false && isGrounded)
            {
                jump = true;
                rb2d.AddForce(new Vector2(0f, jumpForce));
            }
            else
            {
                if(isGrounded || jumpStage == JumpStage.noJumping)
                { 
                    jump = true;
                    rb2d.AddForce(new Vector2(0f, jumpForce));
                    jumpStage = JumpStage.firstJump;
                    Debug.Log("Jump stage: firstJump");
                } else if(isGrounded == false && jumpStage == JumpStage.firstJump)
                {
                    jump = true;
                    rb2d.AddForce(new Vector2(0f, second_jumpForce));
                    jumpStage = JumpStage.secondJump;
                    Debug.Log("Jump stage: secondJump");

                    ResetJumpAnimation();
                } else
                {
                    //Do nothing, cant jump
                    Debug.Log("Jump stage: Max jumps reached");
                }
            }
        }

        //For testing
        if(Input.GetKey("w"))
        {
            //anim.Play("Player_Jump");
        }
        anim.SetBool("IsJumping", jump);
    }

    private void ResetJumpAnimation()
    {
        //Reset Animation
        //anim.Play("Player_Jump");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        jump = false;
        if(canDoubleJump)
        {
            jumpStage = JumpStage.noJumping;
        }
        Debug.Log("Collision with " + collision.gameObject.name);
    }

    void OncollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        if (canDoubleJump)
        {
            jumpStage = JumpStage.noJumping;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log("Collision stopped with " + collision.gameObject.name);
    }
}
