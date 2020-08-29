using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
	protected bool isPlayerMinAgroRange;

	public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
	{
        this.attackPosition = attackPosition;
	}

	public override void DoChecks()
	{
		base.DoChecks();
		isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter()
	{
		base.Enter();
		entity.atsm.attackState = this;
        isAnimationFinished = false;
		entity.SetVelocity(0f);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

    public virtual void TriggerAttack()
    {

    }

    //This function is called in animation
    //ref 13:35 ~ [https://www.youtube.com/watch?v=Qp38X9TEVqI&list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&index=17]
	//ref 38:00 ~ [https://www.youtube.com/watch?v=Qp38X9TEVqI&list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&index=17]
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
