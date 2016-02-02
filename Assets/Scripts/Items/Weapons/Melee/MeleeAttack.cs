// Make knockback from ATTACK rather than from HERO

using UnityEngine;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour
{
    protected Vector2 vel;
    protected float timer = 0.1f;
    protected float knockBack = 5f;
    protected AttackStats stats;
    // Hero transform
    protected Transform chr;

    // Bounds of the attack
    protected Collider2D col;

    // List of objects already hit
    private List<GameObject> alreadyHit;

    // Use this for initialization
    protected virtual void Start()
    {
        // Start a new list
        alreadyHit = new List<GameObject>();

        col = GetComponent<Collider2D>();

        stats = gameObject.GetComponent<AttackStats>();
        knockBack = stats.KnockBack;
        chr = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        timer -= Time.deltaTime;

        // Find all mobs
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");

        // Check if each mob is in the radius
        foreach (GameObject mob in mobs)
        {
            // IF the mob is in the circle
            if (col.IsTouching(mob.GetComponent<Collider2D>()) &&
                !alreadyHit.Contains(mob))
            {
                alreadyHit.Add(mob);
                // Find a vector from the hero to the enemy
                Vector2 pPos = transform.position;
                Vector2 ePos = mob.transform.position;

                vel = (ePos - pPos).normalized * knockBack;

                mob.GetComponent<MobController>().Hit(stats.Damage, chr, this.vel);

                Debug.Log("HIT " + mob.name);
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(mob.GetComponent<Transform>(), stats.Damage);
            }
        }

        // Find all destructables
        GameObject[] destructs = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject destruct in destructs)
        {
            // IF the destructable is in the circle
            if (col.IsTouching(destruct.GetComponent<Collider2D>()) &&
                !alreadyHit.Contains(destruct))
            {
                alreadyHit.Add(destruct);
                destruct.GetComponent<DestructableController>().Hit(stats.Damage, chr, this.vel);

                Debug.Log("HIT " + destruct.name);
                // Raise the event that an destructable was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(destruct.GetComponent<Transform>(), stats.Damage);
            }
        }
        
    }

    /*
    protected void OnTriggerEnter2D(Collider2D c)
    {
        c.bounds.Contains(c.transform.position);
        if (c.CompareTag("Mob"))
        {
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), stats.Damage);

                // Find a vector from the hero to the enemy
                Vector2 pPos = GameObject.FindGameObjectWithTag("Hero").transform.position;
                Vector2 ePos = c.transform.position;

                vel = (ePos - pPos).normalized * knockBack;

                c.GetComponent<MobController>().Hit(stats.Damage, chr, this.vel);
            }
        }
    }*/
}
