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

    // List of objects already hit
    private List<GameObject> alreadyHit;

    // Use this for initialization
    void Start()
    {
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
        Bounds bounds = GetComponent<CircleCollider2D>().bounds;
        // Check if each mob is in the radius
        foreach (GameObject mob in mobs)
        {
            // IF the mob is in the circle
            if (bounds.Contains(mob.transform.position))
            {
                // Find a vector from the hero to the enemy
                Vector2 pPos = transform.position;
                Vector2 ePos = mob.transform.position;

                vel = (ePos - pPos).normalized * knockBack;

                mob.GetComponent<MobController>().Hit(damage, chr, this.vel);

                Debug.Log("HIT " + mob.name);
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(mob.GetComponent<Transform>(), damage);
            }
            else
            {
                print(bounds.ToString() + " || " + mob.transform.position.ToString());
            }
        }

        // Find all destructables
        GameObject[] destructs = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject destruct in destructs)
        {
            // IF the destructable is in the circle
            if (bounds.Contains(destruct.transform.position)&&
                !alreadyHit.Contains(destruct))
            {
                alreadyHit.Add(destruct);
                destruct.GetComponent<DestructableController>().Hit(damage, chr, this.vel);

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
