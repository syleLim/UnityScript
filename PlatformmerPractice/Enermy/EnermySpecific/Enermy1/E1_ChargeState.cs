﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
	private E1 enermy;

	public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, stateData)
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

		if (!isDetectingLedge || isDetectingWall)
		{
			stateMachine.ChangeState(enermy.lookForPlayerState);
		}
		else if (performCloseRangeAction)
		{
				stateMachine.ChangeState(enermy.miliAttackState);
		}
		else if (isChargeTimeOver)
		{
			if (isPlayerInMinAgroRange)
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

	public override string ToString()
	{
		return base.ToString();
	}
}
