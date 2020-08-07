using System.Collections;
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
    public float groundCheckRadius;
    public int amountOfJumps = 2;

    protected Rigidbody2D rb2D;
    protected Animator anim;
    protected float moveDirection;
    public Transform groundCheck;
    public LayerMask WhatIsGround;
    
    private bool isFacingRight;
    private bool isWalking;
    private bool isGrounded;
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
    }

    private void FixedUpdate() {
        applyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
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
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 100.0f, 0.0f);
    }

    protected void applyMovement()
    {
        rb2D.velocity = new Vector2(velocity * moveDirection, rb2D.velocity.y);
    }

    protected void UpdateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        //Seting downward animation : 15:40 in https://www.youtube.com/watch?v=MReoItM8BoI
        anim.SetFloat("yVelocity", rb2D.velocity.y);
    }
}
