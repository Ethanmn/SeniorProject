using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	private Vector2 vel;
	private int damage;

	// Use this for initialization
	void Start () {
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		vel = gameObject.GetComponent<Rigidbody2D>().velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Enemy"))
		{
			Debug.Log("BANG!");
			c.GetComponent<BlobController>().Hit(this.damage, this.vel);

			GameObject.Destroy(gameObject);
		}
	}

	public void SetVel(Vector2 vel)
	{
		this.vel = vel;
	}

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}
}
