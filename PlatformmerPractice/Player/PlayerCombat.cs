using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attackRadius, attackDamage;
    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private LayerMask whttIsDamageable;

    private bool gotInput;
    private bool iSAttacking;
    private bool isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    //enermy damaged
    private float[] attackDetails;
    private PlayerPlatformAdvenced PC;
    private PlayerState PS;

    private void Start() {
        anim =  GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerPlatformAdvenced>();
        PS = GetComponent<PlayerState>();
    }

    private void Update() {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (combatEnabled)
            {
                // Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //Perform Attack1
            if (!iSAttacking)
            {
                gotInput = false;
                iSAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("fistAttack", isFirstAttack);
                anim.SetBool("isAttacking", iSAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            // Wait for new input
            gotInput = false;
        }
    }

    private void Damage(float[] attackDetails)
    {
        if (!PC.GetDashState())
        {
            int direction;
        
            PS.DecreaseHealth(attackDetails[0]);
            if (attackDetails[1] < transform.position.x)
                direction = 1;
            else
                direction = -1;
        
            PC.Knockback(direction);    
        }        
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whttIsDamageable);

        attackDetails[0] = attackDamage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //Instantiate hit particle;
        }
    }

    private void FinishAttack1()
    {
        iSAttacking = false;
        anim.SetBool("isAttacking", iSAttacking);
        anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }
}
