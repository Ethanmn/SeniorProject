using UnityEngine;
using System;
using System.Collections.Generic;

class SparkAttack : MonoBehaviour
{
    protected float timer = 1f;
    protected AttackStats stats;
    // Spark transform
    protected Transform spark;
    public Transform Spark
    {
        get { return spark; }
        set { spark = value; }
    }

    // Collider of the attack
    protected Collider2D col;

    // List of objects already hit
    private List<GameObject> alreadyHit;

    // Use this for initialization
    protected virtual void Start()
    {
        // Start a new list
        alreadyHit = new List<GameObject>();

        col = GetComponent<CircleCollider2D>();

        stats = gameObject.GetComponent<AttackStats>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        timer -= Time.deltaTime;

        // Check if anything is in the trigger
        // Find the hero
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero && col.IsTouching(hero.GetComponent<Collider2D>()) &&
            Vector2.Distance(transform.parent.position, hero.transform.position) > 1.25f)
        {
            hero.GetComponent<HeroController>().Hit(stats.Damage, spark, Vector2.zero);
        }

        // Find all destructables
        GameObject[] destructs = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject destruct in destructs)
        {
            // IF the destructable is in the circle
            if (col.IsTouching(destruct.GetComponent<Collider2D>()) &&
                Vector2.Distance(transform.position, destruct.transform.position) > 1.25f &&
                !alreadyHit.Contains(destruct))
            {
                alreadyHit.Add(destruct);
                destruct.GetComponent<DestructableController>().Hit(stats.Damage, spark, Vector2.zero);
            }
        }
    }

    // For eating projectiles
    void OnTriggerEnter2D(Collider2D c)
    {
        // IF it is an attack from a player AND it is a projectile
        if (c.gameObject.CompareTag("Attack") && c.GetComponent<RangedAttack>() != null)
        {
            // Eat it
            Destroy(c.gameObject);
        }
    }
}
