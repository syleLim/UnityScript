using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : StunState
{
    private E2 enermy;
    
	public E2_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, E2 enermy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            if (isPlayerMinAgroRange)
                stateMachine.ChangeState(enermy.playerDetectedState);
            else
                stateMachine.ChangeState(enermy.lookForPlayerState);
        }
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
