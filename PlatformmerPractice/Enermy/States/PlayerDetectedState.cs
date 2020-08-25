using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerMinAgroRange;
    protected bool isPlayerMaxAgroRange;
	protected bool performLongRangeAction;

	public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
	{
        this.stateData = stateData;
	}

	public override void Enter()
	{
		base.Enter();

		performLongRangeAction = false;
		entity.SetVelocity(0f);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time >= startTime + stateData.longRangeActionTime)
		{
			performLongRangeAction = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
		DoChecks();
	}

	public override void DoChecks()
	{
		base.DoChecks();
		isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
	}
}
