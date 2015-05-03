using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private float speed;
	private I_PlayerState state;
	private int maxAmmo;
	public int ammo;

	// Use this for initialization
	void Start ()
	{
		state = gameObject.GetComponent<PlayerController>().getState();
		speed = 15.0f;
		maxAmmo = 6;
		ammo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = gameObject.GetComponent<PlayerController>().getState();
		if (!state.GetType().Equals(typeof(PlayerStateRoll)))
		{
			if (Input.GetKeyDown(KeyCode.R) && ammo < maxAmmo)
			{
				gameObject.GetComponent<PlayerController>().SetState(new PlayerStateReload());
			}
			if (Input.GetMouseButtonUp(0) && ammo > 0)
			{
				Vector2 pPos = gameObject.GetComponent<Rigidbody2D>().position;
				Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				Vector2 vel = (mPos - pPos).normalized * speed;
				
				GameObject b = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Bullet"));
				b.gameObject.GetComponent<Rigidbody2D>().position = pPos;
				b.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
				b.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BulletPH")[0];

				ammo--;
			}
		}
	}

	public int GetMaxAmmo()
	{
		return this.maxAmmo;
	}
}
