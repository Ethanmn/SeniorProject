// Make knockback from ATTACK rather than from HERO

using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    protected Vector2 vel;
    protected float timer = 0.1f;
    protected float knockBack = 5f;
    protected AttackStats stats;
    // Hero transform
    protected Transform chr;

    // Use this for initialization
    protected virtual void Start()
    {
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
    }

    protected void OnTriggerEnter2D(Collider2D c)
    {
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
    }
}
