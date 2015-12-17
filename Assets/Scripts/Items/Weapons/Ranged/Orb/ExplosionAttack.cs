// Sub-attack only used for "OrbAttack"
// Make knockback from ATTACK rather than from HERO

using UnityEngine;
using System.Collections;

public class ExplosionAttack : MonoBehaviour
{

    private Vector2 vel;
    private float timer = 0.1f;
    public float knockBack = 5f;
    public int damage = 1;
    // Hero transform for Hit()
    private Transform chr;

    // Use this for initialization
    void Start()
    {
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

        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        float radius = GetComponent<CircleCollider2D>().radius;
        foreach (GameObject mob in mobs)
        {
            // IF the mob is in the circle
            if ((Mathf.Pow((mob.transform.position.x - transform.position.x), 2) + 
                Mathf.Pow((mob.transform.position.y - transform.position.y), 2)) 
                < Mathf.Pow(radius, 2))
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
