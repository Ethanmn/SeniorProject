using UnityEngine;

public class OrbAttack : MonoBehaviour {
	
    private AttackStats stats;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<AttackStats>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        Debug.Log("Orb col " + c.gameObject.name);
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
