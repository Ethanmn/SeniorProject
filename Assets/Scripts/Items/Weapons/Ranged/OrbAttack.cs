using UnityEngine;

public class OrbAttack : MonoBehaviour {
	
	private Vector2 vel;
    private AttackStats stats;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<AttackStats>();
	}
	
	// Update is called once per frame
	void Update () {
		vel = gameObject.GetComponent<Rigidbody2D>().velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        // If the projectile hits something
		if ((c.CompareTag("Mob") && !c.GetComponent<MobStats>().Dead) || c.CompareTag("Wall"))
		{
            // Create an explosion
            Vector2 pos = transform.position;
            Quaternion rot = Quaternion.identity;

            GameObject explosion = Object.Instantiate(Resources.Load("Prefabs/OrbExplosion") as GameObject, pos, rot) as GameObject;
            explosion.GetComponent<ExplosionAttack>().damage = stats.Damage;
            explosion.GetComponent<ExplosionAttack>().knockBack = stats.KnockBack;
            GameObject.Destroy(gameObject);
		}
	}
}
