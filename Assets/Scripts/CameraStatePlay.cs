using UnityEngine;
using System.Collections;

public class CameraStatePlay : I_CameraState {

	private float speed;
	private GameObject player;

	void I_CameraState.OnEnter(Transform camera)
	{
		speed = 0.15f;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void I_CameraState.OnExit(Transform camera)
	{

	}
	
	// Update is called once per frame
	I_CameraState I_CameraState.Update(Transform camera, float dt)
	{
		Vector2 pPos = new Vector2(player.transform.position.x, 
	                               player.transform.position.y);
		Vector2 cPos = camera.position;
		Vector2 vel = (pPos - cPos) * 0.25f;

		Vector2 temp = cPos + vel;

		if (Mathf.Abs(temp.x) > Mathf.Abs(pPos.x) ||
		    (Mathf.Abs(pPos.x) - Mathf.Abs(temp.x)) < 0.08f)
		{
			temp.x = pPos.x;
		}
		if (Mathf.Abs(temp.y) > Mathf.Abs(pPos.y) ||
		    (Mathf.Abs(pPos.y) - Mathf.Abs(temp.y)) < 0.08f)
		{
			temp.y = pPos.y;
		}

		camera.position = new Vector3(temp.x, temp.y, -10f);

		return null;
	}

	I_CameraState I_CameraState.HandleInput(Transform camera)
	{
		return null;
	}

	I_CameraState I_CameraState.OnCollisionEnter(Transform camera, Collision2D c)
	{
		return null;
	}
}
