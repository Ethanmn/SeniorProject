// Sub-attack only used for "OrbAttack"
// Make knockback from ATTACK rather than from HERO

using UnityEngine;
using System.Collections.Generic;

public class ExplosionAttack : MonoBehaviour
{
    private Vector2 vel;
    private float timer = 0.1f;
    public float knockBack = 5f;
    public int damage = 1;
    // Hero transform for Hit()
    private Transform chr;

    private Collider2D col;

    // List of objects already hit
    private List<GameObject> alreadyHit;

    // Use this for initialization
    void Start()
    {
        // Get the collider
        col = GetComponent<Collider2D>();
        // Start a new list
        alreadyHit = new List<GameObject>();

        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero != null)
        {
            chr = hero.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Keep track of the explosion's life
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

                mob.GetComponent<MobController>().Hit(damage, chr, this.vel);

                Debug.Log("HIT " + mob.name);
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(mob.GetComponent<Transform>(), damage);
            }
        }

        // Find all destructables
        // Due to Unity's physics engine, we must search for destructables differently for orb explosions than other attacks

        GameObject[] destructs = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject destruct in destructs)
        {
            // IF the destructable is in the circle
            if (col.IsTouching(destruct.GetComponent<Collider2D>()) &&
                !alreadyHit.Contains(destruct))
            {
                alreadyHit.Add(destruct);
                destruct.GetComponent<DestructableController>().Hit(damage, chr, Vector2.zero);

                Debug.Log("HIT " + destruct.name);
                // Raise the event that an destructable was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(destruct.GetComponent<Transform>(), damage);
            }
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("TRIGGERED!!");
        if (c.CompareTag("Mob"))
        {
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Find a vector from the hero to the enemy
                Vector2 pPos = transform.position;
                Vector2 ePos = c.transform.position;

                vel = (ePos - pPos).normalized * knockBack;

                c.GetComponent<MobController>().Hit(damage, chr, this.vel);

                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), damage);
            }
        }
    }*/
}
