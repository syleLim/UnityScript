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
        Moving,
        Knockback,
        Dead
    }
    private State currentState;

    // Object Detection
    [SerializeField]
    private Transform groundCheck, wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float groundCheckDistance, wallCheckDistance;
    private bool groundDetected, wallDetected;
    
    // GameObj
    private GameObject alive;
    private Rigidbody2D aliveRb2D;
    private Animator aliveAnim;

    // Movement
    [SerializeField]
    private int facingDirection;
    private float movementSpeed;
    private Vector2 movement;

    // HP
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    // Knockback
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private float knockbackDuration;
    private float knockbackStartTime;
    
    // Damage
    private int damageDirection;
    [SerializeField]
    private GameObject hitParticle, deathChunkParticle, deathBloodParticle;
    


    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb2D = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();
        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Moving :
                UpdateMovingState();
                break;

            case State.Knockback :
                UpdateKnockbackState();
                break;
            
            case State.Dead :
                UpdateDeadState();
                break;
        }
    }

    //----Moving State----
    private void EnterMovingState()
    {
 
    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
            Flip();
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb2D.velocity.y);
            aliveRb2D.velocity = movement;   
        }
    }

    private void ExitMovingState()
    {

    }

    //----Knocback State----
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb2D.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
            SwitchState(State.Moving);
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    //----Dead State----
    private void EnterDeadState()
    {
        Destroy(gameObject);
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
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
            case State.Moving :
                ExitMovingState();
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
            case State.Moving :
                EnterMovingState();
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

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        //Make particle
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 180.0f)));
        
        //Set knockback direction
        if (attackDetails[1] > alive.transform.position.x)
            damageDirection = -1;
        else
            damageDirection = 1;

        if (currentHealth > 0.0f)
            SwitchState(State.Knockback);
        else
            SwitchState(State.Dead);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
