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

			/* Ranged Attack */
			if (Input.GetMouseButtonUp(0))
			{
				Vector2 pPos = gameObject.GetComponent<Rigidbody2D>().position;
				Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				if (ammo > 0)
				{
					Vector2 vel = (mPos - pPos).normalized * speed;
					
					GameObject b = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Bullet"));
					b.gameObject.transform.position = new Vector3(pPos.x, pPos.y, 0f);
					b.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
					b.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BulletPH")[0];
					
					ammo--;
				}
			}

			/* Melee attack */
			if (Input.GetMouseButtonUp(1))
			{
				Vector2 pPos = gameObject.GetComponent<Transform>().position;
				Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				Vector2 dir = (mPos - pPos).normalized;
				float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				
				if (ang < 0)
					ang += 360f;

				int fixedAng = Mathf.RoundToInt(ang / 45) * 45;
				Vector2 offSet = Quaternion.Euler(0, 0, fixedAng) * new Vector2(0.4f, 0);
				
				GameObject s = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/PlayerSlash"));
				s.transform.parent = gameObject.transform;
				s.transform.Rotate(0, 0, fixedAng);
				s.gameObject.transform.position = new Vector3(pPos.x + offSet.x, pPos.y + offSet.y, 0f);
			}
		}
	}

	public int GetMaxAmmo()
	{
		return this.maxAmmo;
	}
}
