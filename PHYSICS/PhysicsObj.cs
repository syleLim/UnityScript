using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsObj : MonoBehaviour {
	public float gravityModifier = 1f;
	
	protected Rigidbody2D rb2D;
	protected Vector2 velocity;

	private void Start() {
		rb2D = GetComponent<Rigidbody2D>();		
	}

	private void FixedUpdate() {
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 move = Vector2.up * deltaPosition.y;
		Movement(move);
	}

	protected void Movement(Vector2 move)
	{
		rb2D.position += move;
	}

}