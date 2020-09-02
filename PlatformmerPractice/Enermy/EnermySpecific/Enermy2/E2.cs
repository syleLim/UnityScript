using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
	public E2_PlayerDetectedState playerDetectedState { get; private set ;}
	public E2_MiliAttackState miliAttackState { get; private set; }
	public E2_LookForPlayerState lookForPlayerState { get; private set; }
	public E2_StunState stunState { get; private set; }
	public E2_DeadState deadState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateDate;
	[SerializeField]
	private D_PlayerDetectedState playerDetectedStateData;
	[SerializeField]
	private D_MiliAttackState miliAttackStateData;
	[SerializeField]
	private D_LookForPlayerState lookForPlayerStateData;
	[SerializeField]
	private D_StunState stunStateData;
	[SerializeField]
	private D_DeadState deadStateData;

	[SerializeField]
	private Transform miliAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateDate, this);
		playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
		miliAttackState = new E2_MiliAttackState(this, stateMachine, "miliAttack", miliAttackPosition, miliAttackStateData, this);
		lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
		stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
		deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.initialize(moveState);
    }

	public override void Update()
	{
		base.Update();
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	public override void Flip()
	{
		base.Flip();
	}

	public override void Damage(AttackDetails attackDetails)
	{
		base.Damage(attackDetails);

		if (isDead)
			stateMachine.ChangeState(deadState);
		else if (isStunned && stateMachine.currentState != stunState)
			stateMachine.ChangeState(stunState);
		else if (!CheckPlayerInMinAgroRange())
		{
			lookForPlayerState.SetTurnImeddiately(true);
			stateMachine.ChangeState(lookForPlayerState);
		}
		
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
	}
}
