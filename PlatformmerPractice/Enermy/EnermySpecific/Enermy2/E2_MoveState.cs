using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
	protected E2 enermy;

	public E2_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, E2 enermy) : base(entity, stateMachine, animBoolName, stateData)
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

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isPlayerMinAgroRange)
			stateMachine.ChangeState(enermy.playerDetectedState);
		else if (isDetectingWall || !isDetectingLedge)
		{
			enermy.idleState.SetFlipAfterIdle(true);
			stateMachine.ChangeState(enermy.idleState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
