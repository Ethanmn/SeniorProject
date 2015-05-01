using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	I_PlayerState state = this.gameObject.GetComponent<PlayerController>.getState();

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown && !state.GetType().Equals(typeof(PlayerStateRoll)))
		{
			Vector3 mPos = Input.mousePosition;
			Vector3 pPos = gameObject.GetComponent<Rigidbody2D>().position;

			Vector3 vel = Vector3.Normalize(new Vector2(mPos - pPos));

		}
	}
}
