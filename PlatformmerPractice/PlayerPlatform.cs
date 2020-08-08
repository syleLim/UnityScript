﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   This script work with anim and material setting,
*   anim : 15:40 ~ https://www.youtube.com/watch?v=MReoItM8BoI
*   material : 19:00 ~ https://www.youtube.com/watch?v=MReoItM8BoI
*/
public class PlayerPlatform : MonoBehaviour
{
    /*
    *   This is one of default Setting
    *   Must check not rotate in rb2d.
    *   
    */
    private const float velocity = 10f;
    private const float jumpForce = 2f;
    public int amountOfJumps = 2;
    public float wallSlidingSpeed;
    public float groundCheckRadius;
    public float wallCheckDistance;
    
    

    protected Rigidbody2D rb2D;
    protected Animator anim;
    protected float moveDirection;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask WhatIsGround;
    
    private bool isFacingRight;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private int amountOfJumpsLeft;


    private void OnEnable() {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isFacingRight = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckIfJump();
        CheckIfWallSliding();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb2D.velocity.y < 0)
            isWallSliding = true;
        else
            isWallSliding = false;
    }

    private void FixedUpdate() {
        applyMovement();
        CheckSurroundings();
    }

    protected void CheckInput()
    {
        moveDirection = Input.GetAxisRaw("horizontal");

        if (Input.GetButtonDown("jump"))
        {
            Jump();
        }
    }

    protected void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        amountOfJumpsLeft--;
    }

    /*
    *   For Ground Check. It is called grounding(접지)
    *   Overlap checks objs in setting layer. 
    */
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        
        // Check Wall with right direction 
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
    }

    private void CheckIfJump()
    {
        if (isGrounded && rb2D.velocity.y <= 0)
            amountOfJumpsLeft = amountOfJumps;
        
        if (amountOfJumpsLeft <= 0)
            canJump = false;
        else
            canJump = true;
    }

    protected void CheckMovementDirection()
    {
        if (isFacingRight && moveDirection < 0)
            Flip();
        else if (!isFacingRight && moveDirection > 0)
            Flip(); 
        
        if (rb2D.velocity.x != 0)
            isWalking = true;
        else
            isWalking = false;

    }

    protected void Flip()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 100.0f, 0.0f);
        }
    }


    // Draw Area for checking objs(grounding) in Scene(not play)
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    protected void applyMovement()
    {
        if (isGrounded)
            rb2D.velocity = new Vector2(velocity * moveDirection, rb2D.velocity.y);

        //Wall Slide session
        if (isWallSliding)
        {
            if (rb2D.velocity.y < -wallSlidingSpeed)
                rb2D.velocity = new Vector2(rb2D.velocity.x, -wallSlidingSpeed);
        }
    }

    protected void UpdateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        //Seting downward animation : 15:40 in https://www.youtube.com/watch?v=MReoItM8BoI
        anim.SetFloat("yVelocity", rb2D.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }
}
