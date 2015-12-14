// Knockback needs to be based on stats.Knockback
// Script for bullets / arrows

using UnityEngine;

public class RangedAttack : MonoBehaviour {
	
	private Vector2 vel;
    private AttackStats stats;
    // Hero transform for Hit()
    private Transform chr;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<AttackStats>();

        chr = GameObject.FindGameObjectWithTag("Hero").transform;
    }
	
	// Update is called once per frame
	void Update () {
		vel = gameObject.GetComponent<Rigidbody2D>().velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        if (c.CompareTag("Mob"))
        {
            MobController mCon = c.GetComponent<MobController>();
            if (!c.GetComponent<MobStats>().Dead && !mCon.State.GetType().Equals(typeof(I_MobFlinchState)))
            {
                c.GetComponent<MobController>().Hit(stats.Damage, chr, (this.vel / 7f));
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), stats.Damage);

                // Remove the projectile
                Destroy(gameObject);
            }
            
        }
        // IF the projectile hits a wall
        else if (c.CompareTag("Wall"))
        {
            //Debug.Log("HIT A WALL");
            // Remove the projectile
            Destroy(gameObject);
        }
    }
}
