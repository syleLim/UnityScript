using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
	private E1 enermy;

	public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, stateData)
	{
		this.enermy = enermy;
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
		{
			stateMachine.ChangeState(enermy.playerDetectedState);
		}

		if (isDetectingWall || !isDetectingLedge)
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
