﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private E1 enermy;

	public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performLongRangeAction)
        {
            //enermy.idleState.SetFlipAfterIdle(false); -> not sure
            stateMachine.ChangeState(enermy.chargeState);
        }
		else if(performLongRangeAction)
		{
			stateMachine.ChangeState(enermy.miliAttackState);
		}
		else if (!isPlayerMinAgroRange)
		{
			stateMachine.ChangeState(enermy.lookForPlayerState);
		}
		else if (!isDetectedLedge)
		{
			entity.Flip();
			stateMachine.ChangeState(enermy.moveState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
