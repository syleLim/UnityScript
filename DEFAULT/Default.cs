using UnityEngine;
using System.Collections;

public class Default : MonoBehaviour {
	/*
	*	When Scene load
	*/
	//Instantly prefab instance active
	private void Awake() {
		
	}

	//Instantly objs active(everytime work)
	private void OnEnable() {
		
	}


	/*
	*	When before first Update
	*/
	private void Start() {

	}

	/*
	*	logic methods
	*/
	//Every frame
	private void Update() {
		
	}
	//Fixed frame <- For Physics update
	private void FixedUpdate() {
		
	}
	//After Update(); <- For Cam moving
	private void LateUpdate() {
		
	}

	/*
	*	When objs destroyed
	*/
	private void OnDestroy() {
		
	}

	/*
	*	When Scene end
	*/
	private void OnDisable() {
		
	}
}
