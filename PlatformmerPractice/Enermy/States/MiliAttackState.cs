using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliAttackState : AttackState
{
    protected D_MiliAttackState stateData;
	protected AttackDetails attackDetails;

	public MiliAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MiliAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
	{
        this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();
		attackDetails.damageAmount = stateData.attackDamage;
		attackDetails.position = entity.aliveGameObject.transform.position;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void FinishAttack()
	{
		base.FinishAttack();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void TriggerAttack()
	{
		base.TriggerAttack();

        Collider2D[] detectedObject = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach(var collider in detectedObject)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
	}
}
