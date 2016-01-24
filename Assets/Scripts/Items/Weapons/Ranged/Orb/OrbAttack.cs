using UnityEngine;

public class OrbAttack : RangedAttack {
	
    private AttackStats stats;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<AttackStats>();
	}

    public override void OnTriggerEnter2D(Collider2D c)
    {

    }

    void OnTriggerStay2D(Collider2D c)
	{
        // If the projectile hits something
		if ((c.CompareTag("Mob") && !c.GetComponent<MobStats>().Dead))
		{
            // Create an explosion
            Vector2 vel = GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 spriteSize = c.GetComponent<SpriteRenderer>().sprite.bounds.extents;

            // Move it to the edge of the thing hit
            //Vector2 pos = c.transform.position;
            //Vector2 pos = (Vector2)c.transform.position - new Vector2(spriteSize.x * vel.x, spriteSize.y * vel.y);
            //Vector2 pos = transform.position;
            Vector2 pos = (Vector2)transform.position - new Vector2(spriteSize.x * vel.x, spriteSize.y * vel.y);
            Quaternion rot = transform.rotation;

            GameObject explosion = Object.Instantiate(Resources.Load("Prefabs/OrbExplosion") as GameObject, pos, rot) as GameObject;
            explosion.GetComponent<ExplosionAttack>().damage = stats.Damage;
            explosion.GetComponent<ExplosionAttack>().knockBack = stats.KnockBack;
            GameObject.Destroy(gameObject);
		}
        else if (c.CompareTag("Wall"))
        {
            Vector2 pos = transform.position;
            Quaternion rot = transform.rotation;

            GameObject explosion = Object.Instantiate(Resources.Load("Prefabs/OrbExplosion") as GameObject, pos, rot) as GameObject;
            explosion.GetComponent<ExplosionAttack>().damage = stats.Damage;
            explosion.GetComponent<ExplosionAttack>().knockBack = stats.KnockBack;
            GameObject.Destroy(gameObject);
        }
        else if (c.CompareTag("Destructable"))
        {
            Vector2 pos = transform.position;
            Quaternion rot = transform.rotation;

            GameObject explosion = Object.Instantiate(Resources.Load("Prefabs/OrbExplosion") as GameObject, pos, rot) as GameObject;
            explosion.GetComponent<ExplosionAttack>().damage = stats.Damage;
            explosion.GetComponent<ExplosionAttack>().knockBack = stats.KnockBack;
            GameObject.Destroy(gameObject);
        }
	}
}
