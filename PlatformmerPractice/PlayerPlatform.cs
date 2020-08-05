using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    /*
    *   This is one of default Setting
    *   Must check not rotate in rb2d.
    *   
    */
    private const float velocity = 10f;
    private const float jumpForce = 2f;

    protected Rigidbody2D rb2D;
    protected float moveDirection;
    private bool isFacingRight;

    private void OnEnable() {
        rb2D = GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    private void FixedUpdate() {
        applyMovement();
    }

    protected void CheckInput()
    {
        moveDirection = Input.GetAxisRaw("horizontal");

        if (Input.GetButtonDown("jump"))
        {

        }
    }

    protected void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumFroce);
    }

    protected void CheckMovementDirection()
    {
        if (isFacingRight && moveDirection < 0)
            Flip();
        else if (!isFacingRight && moveDirection > 0)
            Flip(); 
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
}
