using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnermyController : MonoBehaviour
{
    /*
    *   enum makes Macro sucessive number.
    */
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }
    private State currentState;

    [SerializeField]
    private Transform groundCheck, wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float groundCheckDistance, wallCheckDistance;

    private bool groundDetected, wallDetected;
    private int facingDirection;
    
    private GameObject alive;
    private Rigidbody2D aliveRb2D;

    [SerializeField]
    private float movementSpeed;
    private Vector2 movement;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb2D = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Walking :
                UpdateWalkingState();
                break;

            case State.Knockback :
                UpdateKnockbackState();
                break;
            
            case State.Dead :
                UpdateDeadState();
                break;
        }
    }

    //----Walking State----
    private void EnterWalkingState()
    {
 
    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        if (groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb2D.velocity.y);
            aliveRb2D.velocity = movement;

            
        }
    }

    private void ExitWalkingState()
    {

    }

    //----Knocback State----
    private void EnterKnockbackState()
    {
 
    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //----Dead State----
    private void EnterDeadState()
    {
 
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //----Other function----
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking :
                ExitWalkingState();
                break;
            
            case State.Knockback :
                ExitKnockbackState();
                break;

            case State.Dead :
                ExitDeadState();
                break;
        }

        switch (state)
         {
            case State.Walking :
                EnterWalkingState();
                break;
            
            case State.Knockback :
                EnterKnockbackState();
                break;

            case State.Dead :
                EnterDeadState();
                break;
         }

         currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void Move()
    {

    }

}
