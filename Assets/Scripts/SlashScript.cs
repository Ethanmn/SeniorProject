using UnityEngine;
using System.Collections;

public class SlashScript : MonoBehaviour {

	private Vector2 vel;
	private int damage;
	private float timer = 0.1f;
	
	// Use this for initialization
	void Start () {
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0)
		{
			GameObject.Destroy(gameObject);
		}
		timer -= Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Enemy"))
		{
			Debug.Log("SWING!");

			Vector2 pPos = GameObject.FindGameObjectWithTag("Player").transform.position;
			Vector2 ePos = c.transform.position;

			vel = (ePos - pPos).normalized * 3f;

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
