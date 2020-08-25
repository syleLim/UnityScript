using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private E1 enermy;

	public E1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enermy.moveState);
        }
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}	
}