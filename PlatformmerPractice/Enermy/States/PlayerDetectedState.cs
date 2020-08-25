using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerMinAgroRange;
    protected bool isPlayerMaxAgroRange;

	public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
	{
        this.stateData = stateData;
	}

	public override void Enter()
	{
		base.Enter();

		entity.SetVelocity(0f);

        isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
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
}
