﻿// Knockback needs to be based on stats.Knockback
// Script for bullets / arrows

using UnityEngine;

public class RangedAttack : MonoBehaviour {
	
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
        if (c.CompareTag("Mob"))
        {
            MobController mCon = c.GetComponent<MobController>();
            if (!c.GetComponent<MobStats>().Dead && !mCon.getState().GetType().Equals(typeof(I_MobFlinchState)))
            {
                c.GetComponent<MobController>().Hit(stats.Damage, (this.vel / 7f));
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), stats.Damage);

                // Remove the projectile
                Destroy(gameObject);
            }
            
        }
        // IF the projectile hits a wall
        else if (c.CompareTag("Wall"))
        {
            // Remove the projectile
            Destroy(gameObject);
        }
    }
}
