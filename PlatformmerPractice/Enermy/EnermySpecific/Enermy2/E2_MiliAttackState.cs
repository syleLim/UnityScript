using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MiliAttackState : MiliAttackState
{
    private E2 enermy;

	public E2_MiliAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MiliAttackState stateData, E2 enermy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
                stateMachine.ChangeState(enermy.playerDetectedState);
            else if (!isPlayerMinAgroRange)
				stateMachine.ChangeState(enermy.lookForPlayerState);
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
