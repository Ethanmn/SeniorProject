using UnityEngine;
using System;
using System.Collections.Generic;

class SparkAttack : MonoBehaviour
{
    protected float timer = 1f;
    protected AttackStats stats;
    // Spark transform
    protected Transform chr;

    // Bounds of the attack
    protected Bounds bounds;

    // List of objects already hit
    private List<GameObject> alreadyHit;

    // Use this for initialization
    protected virtual void Start()
    {
        // Start a new list
        alreadyHit = new List<GameObject>();

        bounds = GetComponent<Collider2D>().bounds;
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
        if (hero && bounds.Contains(hero.transform.position) &&
            Vector2.Distance(transform.position, hero.transform.position) > 1.25f)
        {
            // CHANGE TRANSFORM TO PARENT'S TRANSFORM
            hero.GetComponent<HeroController>().Hit(1, transform, Vector2.zero);
        }
        else
        {
            Debug.Log(bounds + " | " + hero.transform.position);
        }
        // Find all destructables
        GameObject[] destructs = GameObject.FindGameObjectsWithTag("Destructable");
        foreach (GameObject destruct in destructs)
        {
            print(destruct.name);
            // IF the destructable is in the circle
            if (bounds.Contains(destruct.transform.position) &&
                Vector2.Distance(transform.position, destruct.transform.position) > 1.25f &&
                !alreadyHit.Contains(destruct))
            {
                alreadyHit.Add(destruct);
                destruct.GetComponent<DestructableController>().Hit(1, transform, Vector2.zero);

                Debug.Log("HIT " + destruct.name);
            }
        }
    }
}
