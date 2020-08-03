using UnityEngine;
using System.Collections;

public abstract class SideScroll : MonoBehaviour {
	Rigidbody2D rb2D;
	BoxCollider2D boxCol2D;

	protected virtual void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		boxCol2D = GetComponent<BoxCollider2D>();
	}

	protected virtual void Update()
	{
		Move(1, 1);
	}

	protected bool Move(float x, float y)
	{
		return true;
	}
}
