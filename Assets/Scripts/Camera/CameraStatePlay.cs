using UnityEngine;
using System.Collections;

public class CameraStatePlay : I_CameraState {
	
	private GameObject hero;
    private float speed = 0.2f;

	void I_CameraState.OnEnter(Transform camera)
	{
		hero = GameObject.FindGameObjectWithTag("Hero");
	}

	void I_CameraState.OnExit(Transform camera)
	{
        
	}
	
	// Update is called once per frame
	I_CameraState I_CameraState.Update(Transform camera, float dt)
	{
        // If you can find a player, move towards it
        if (hero)
        {
            Vector2 pPos = new Vector2(hero.transform.position.x,
                                   hero.transform.position.y);
            Vector2 cPos = camera.position;
            Vector2 vel = (pPos - cPos) * speed;

            Vector2 temp = cPos + vel;

            camera.position = new Vector3(temp.x, temp.y, -10f);
        }
        // Else do nothing

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
