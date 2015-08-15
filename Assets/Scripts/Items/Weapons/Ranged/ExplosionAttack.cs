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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Mob"))
        {
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Find a vector from the hero to the enemy
                Vector2 pPos = GameObject.FindGameObjectWithTag("Hero").transform.position;
                Vector2 ePos = c.transform.position;

                vel = (ePos - pPos).normalized * knockBack;

                c.GetComponent<MobController>().Hit(damage, this.vel);

                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), damage);
            }
        }
    }
}
