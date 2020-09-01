using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    private E1 enermy;

	public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, E1 enermy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enermy.miliAttackState);
            }
            else if (isPlayerMinAgroRange)
            {
                stateMachine.ChangeState(enermy.chargeState);
            }
            else
            {
                enermy.lookForPlayerState.SetTurnImeddiately(true);
                stateMachine.ChangeState(enermy.lookForPlayerState);
            }
        }
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
