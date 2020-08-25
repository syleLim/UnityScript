using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

	protected bool isDetectingWall;
	protected bool isDetectingLedge;
	
	protected bool isPlayerMinAgroRange;

	public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
	{
        this.stateData = stateData;
	}

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

		isDetectingWall = entity.CheckWall();
		isDetectingLedge = entity.CheckLedge();
		isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
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

		isDetectingWall = entity.CheckWall();
		isDetectingLedge = entity.CheckLedge();
		isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}
}
