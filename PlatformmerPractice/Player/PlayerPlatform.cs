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
    private int facingDirection = 1;
    public int amountOfJumps = 2;
    public float wallSlidingSpeed;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float movementForceInAir;
    public float airDragMultiplier = 0.55f;
    public float jumpHighierMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    

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
    

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isFacingRight = true;
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
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

    private void FixedUpdate() {
        applyMovement();
        CheckSurroundings();
    }

    protected void CheckInput()
    {
        moveDirection = Input.GetAxisRaw("horizontal");

        if (Input.GetButtonDown("Jump"))
            Jump();

        // This for change jump tab height;
        if (Input.GetButtonUp("Jump"))
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * jumpHighierMultiplier);

    }

    protected void Jump()
    {
        if (canJump && !isWallSliding) // !isWallSliding is wall jump
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        // Below is for Hopping Wall(Just down ward(not jump))
        else if (isWallSliding && moveDirection == 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb2D.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        // Below is for Jump at Wall
        else if ((isWallSliding || isTouchingWall) && moveDirection != 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveDirection, wallJumpForce * wallJumpDirection.y);
            rb2D.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
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

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb2D.velocity.y < 0)
            isWallSliding = true;
        else
            isWallSliding = false;
    }

    private void CheckIfJump()
    {
        if ((isGrounded && rb2D.velocity.y <= 0) || isWallSliding)
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
        if (!isWallSliding) // This condition for right position for wallSliding.
        {
            facingDirection *= -1;
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
        // This for Charactor while jump, move little different.
        else if (!isGrounded && !isWallSliding && moveDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * moveDirection, 0);
            rb2D.AddForce(forceToAdd);

            if (Mathf.Abs(rb2D.velocity.x) > velocity)
                rb2D.velocity = new Vector2(velocity * moveDirection, rb2D.velocity.y);
        }
        // This for slow down airmove when stop input. move speed slow down.
        else if (!isGrounded && !isWallSliding && moveDirection == 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x * airDragMultiplier, rb2D.velocity.y);
        }
        
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
