using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private PMoveState moveState;
	private bool ice;
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Debug.Log(moveState.GetType());
		moveState.Update(this, Time.deltaTime);
	}

	private void Awake()
	{
		moveState = new PMStandingState();
	}

	// Public Methods:
	public void ChangeMoveState(PMoveState state)
	{
		moveState.OnExit(this);
		moveState = state;
		moveState.OnEnter(this);
	}

}
