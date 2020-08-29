using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MiliAttackState : MiliAttackState
{
    private E1 enermy;

	public E1_AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MiliAttackState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
	{
        this.enermy = enermy;
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();
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
        if (isAnimationFinished)
        {
            if (isPlayerMinAgroRange)
            {
                stateMachine.ChangeState(enermy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enermy.lookForPlayerState);
            }
        }
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void TriggerAttack()
	{
		base.TriggerAttack();
	}
}
