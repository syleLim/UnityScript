using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsObj : MonoBehaviour {
	protected const float	gravityModifier = 1f;		// For gravity value
	protected const float	minGroundNormalY = 0.65f;	// For slide ground
	protected const float	minMoveDistance = 0.001f;	// For check valid move
	protected const float	shellRaius = 0.01f;			// ?
	
	protected bool		grounded;
	protected Vector2	groundedNormal;

	protected Rigidbody2D			rb2D;
	protected Vector2				velocity;
	protected ContactFilter2D		contactFilter;
	protected RaycastHit2D[]		hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D>	hitBufferList = new List<RaycastHit2D>(16);

	private void OnEnable() {
		rb2D = GetComponent<Rigidbody2D>();		
	}
	
	private void Start() {
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	private void FixedUpdate() {
		grounded = false;

		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		Vector2 deltaPosition = velocity * Time.deltaTime;
		Vector2 move = Vector2.up * deltaPosition.y;
		Movement(move, true);
	}

	protected void Movement(Vector2 move, bool yMovement)
	{
		float distance = move.magnitude; 

		if (distance > minMoveDistance)
		{
			int count = rb2D.Cast(move, contactFilter, hitBuffer, distance + shellRaius);
			hitBufferList.Clear();
			for (int i = 0; i < count; i++)
			{
				hitBufferList.Add(hitBuffer[i]);
			}

			//check grounded or not
			for (int i = 0; i< hitBufferList.Count; i++)
			{
				Vector2 currentNormal = hitBufferList[i].normal;
				if (currentNormal.y > minGroundNormalY)
				{
					grounded = true;
					if (yMovement)
					{
						groundedNormal = currentNormal;
						currentNormal.x = 0;
					}
				} 
				float projection = Vector2.Dot(velocity, currentNormal);
				if (projection < 0)
				{
					velocity = velocity - projection * currentNormal;
				}
				float modifiedDistance = hitBufferList[i].distance - shellRaius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}
		rb2D.position += move.normalized * distance;
	}
}