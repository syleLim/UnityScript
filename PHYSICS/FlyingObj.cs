using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   Flying Objects must set rigidBody gravity 0
*   and move only velocity of real objects
*   Move smooth, specially rotate back side.
*   Move toward some moving point.
*/
public class FlyingObj : MonoBehaviour
{
	protected const float	minMoveDistance = 0.001f;
	protected const float	shellRaius = 0.01f;
	protected const float	maxSpeed = 7f;

	protected Rigidbody2D			rb2D;
	protected Vector2				velocity;
	protected ContactFilter2D		contactFilter;
	protected GameObject			targetObject;

	protected RaycastHit2D[]		hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D>	hitBufferList = new List<RaycastHit2D>(16);

	// Start is called before the first frame update
	private void OnEnable() 
	{
		rb2D = GetComponent<Rigidbody2D>();
		velocity = Vector2.zero;
	}

	private void Start()
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	// Update is called once per frame
	void Update()
	{
		ComputeVelocity();
	}

	protected virtual void ComputeVelocity()
	{
		
	}

	private void FixedUpdate()
	{
		Vector2 deltaPosition = velocity * Time.deltaTime;
		Vector2 move = deltaPosition;

		Movement(move);
	}

	protected void Movement(Vector2 move)
	{
		
	}
}
