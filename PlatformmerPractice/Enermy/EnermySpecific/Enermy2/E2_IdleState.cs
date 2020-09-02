﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : IdleState
{
    protected E2 enermy;

	public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, E2 enermy) : base(entity, stateMachine, animBoolName, stateData)
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
		else if (isIdleTimeOver)
			stateMachine.ChangeState(enermy.moveState);
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
