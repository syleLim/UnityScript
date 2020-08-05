using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsObj
{
    public float    maxSpeed = 7;
    public float    jumpTakeOffSpeed = 7f;

    private SpriteRenderer  spriteRenderer;
    private Animator        animator;

    // Start is called before the first frame update
    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("horizontal");

        if (Input.GetButtonDown("jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        //cancle jump
        else if (Input.GetButtonUp("jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        //object look right direction(with flipX)
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (MonoBehaviour.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //Set anim
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocity", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
