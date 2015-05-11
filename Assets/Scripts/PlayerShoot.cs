using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	private float speed;
	private I_PlayerState state;
	private int maxAmmo;
	public int ammo;

	public bool gun;

	// Use this for initialization
	void Start ()
	{
		state = gameObject.GetComponent<PlayerController>().getState();
		speed = 15.0f;
		maxAmmo = 6;
		ammo = maxAmmo;
		gun = false;
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
			if (Input.GetMouseButtonUp(0))
			{
				Vector2 pPos = gameObject.GetComponent<Rigidbody2D>().position;
				Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				if (!gun)
				{
					GameObject s = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PlayerSlash"));
					s.transform.parent = gameObject.transform;
					s.gameObject.transform.position = new Vector3(pPos.x + 0.5f, pPos.y, 0f);
				}
				else if (gun && ammo > 0)
				{
					Vector2 vel = (mPos - pPos).normalized * speed;
					
					GameObject b = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Bullet"));
					b.gameObject.transform.position = new Vector3(pPos.x, pPos.y, 0f);
					b.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
					b.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BulletPH")[0];
					
					ammo--;
				}
			}
		}
	}

	public int GetMaxAmmo()
	{
		return this.maxAmmo;
	}
}
