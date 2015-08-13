using UnityEngine;
using System.Collections;

public class SlashScript : MonoBehaviour {

	private Vector2 vel;
	public int damage;
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
        if (c.CompareTag("Mob"))
        {
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), damage);

                // Find a vector from the hero to the enemy
                Vector2 pPos = GameObject.FindGameObjectWithTag("Hero").transform.position;
                Vector2 ePos = c.transform.position;

                vel = (ePos - pPos).normalized * 3f;

                c.GetComponent<MobController>().Hit(this.damage, this.vel);
            }
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
