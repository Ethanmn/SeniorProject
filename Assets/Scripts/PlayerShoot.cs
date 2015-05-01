using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private float speed;
	private I_PlayerState state;

	// Use this for initialization
	void Start ()
	{
		state = gameObject.GetComponent<PlayerController>().getState();
		speed = 40.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = gameObject.GetComponent<PlayerController>().getState();
		if (Input.GetMouseButtonUp(0) && !state.GetType().Equals(typeof(PlayerStateRoll)))
		{
			Vector2 pPos = gameObject.GetComponent<Rigidbody2D>().position;
			Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//Debug.Log(mPos + " - " + pPos + " = " + (mPos - pPos));
			Debug.Log((mPos - pPos).normalized);
			Vector2 vel = (mPos - pPos).normalized * speed;

			GameObject b = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Bullet"));
			b.gameObject.GetComponent<Rigidbody2D>().position = pPos;
			b.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
			b.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BulletPH")[0];
		}
	}
}
