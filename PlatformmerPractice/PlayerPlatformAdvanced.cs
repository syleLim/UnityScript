using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This Project Advanced Move motion.
*/
public class PlayerPlatformAdvenced : MonoBehaviour
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
    private int amountOfJumpsLeft;
    

    /*
    *   For Additional Jump
    */
    private float jumpTimer;
    public float jumpTimerSet = 0.15f;
    private bool isAttemptingToJump;
    private bool canNormalJump;
    private bool canWallJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private float turnTimer;
    private float turnTimerSet = 0.15f;
    private float wallJumpTimer;
    private float wallJumpTimerSet = 0.5f;
    private bool hasWallJumped;
    private int lastWallJumpDirection;
    

    /*
    *   For Edge Climb
    */
    public Transform ledgeCheck;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    

    

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
        CheckJump();
        CheckLedge();
    }

    private void CheckLedge()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, 
                                        Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2,
                                        Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, 
                                        Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2,
                                        Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canMove = true;
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    /*
    *   Call it and of climb animation
    */
    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    private void FixedUpdate() {
        applyMovement();
        CheckSurroundings();
    }

    protected void CheckInput()
    {
        moveDirection = Input.GetAxisRaw("horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                //?? Why?
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        //For protecting Wall Flip and move(...? why move?)
        if (Input.GetButtonDown("horizontal") && isTouchingWall)
        {
            if (!isGrounded && moveDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        /*
        *   change for climb (!canMove -> turnTimer >= 0)
        */
        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        // This for change jump tab height;
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * jumpHighierMultiplier);
        }
    }

    protected void CheckJump()
    {
        if (jumpTimer > 0)
        {
            //wall Jump
            if (!isGrounded && isTouchingWall && moveDirection != 0 && moveDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }
        
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        /*
        *   For Wall Jump again
        */
        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && moveDirection == -lastWallJumpDirection)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    protected void NormalJump()
    {
        if (canNormalJump) // !isWallSliding is wall jump
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }   
    }

    protected void WallJump()
    {
        // Below is for Jump at Wall
        if (canWallJump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveDirection, wallJumpForce * wallJumpDirection.y);
            rb2D.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
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
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, WhatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && moveDirection == facingDirection && rb2D.velocity.y < 0 && !canClimbLedge) //Ofcourse cant slide when climb
            isWallSliding = true;
        else
            isWallSliding = false;
    }

    private void CheckIfJump()
    {
        if (isGrounded && rb2D.velocity.y <= 0.01f)
            amountOfJumpsLeft = amountOfJumps;
        
        if (isTouchingWall)
            canWallJump = true;

        if (amountOfJumpsLeft <= 0)
            canNormalJump = false;
        else
            canNormalJump = true;
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
        if (!isWallSliding && canFlip) // This condition for right position for wallSliding.
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
        // This for slow down airmove when stop input. move speed slow down.
        if (!isGrounded && !isWallSliding && moveDirection == 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x * airDragMultiplier, rb2D.velocity.y);
        }
        else if (canMove)
        {
            rb2D.velocity = new Vector2(velocity * moveDirection, rb2D.velocity.y);
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
