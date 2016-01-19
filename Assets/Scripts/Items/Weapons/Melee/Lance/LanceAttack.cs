using UnityEngine;

/*
    Attack script for a lance. It inherits from MeleeAttack
*/

public class LanceAttack : MeleeAttack
{
    private LanceStats lanStats;

    protected override void Start()
    {
        base.Start();

        lanStats = gameObject.GetComponent<LanceStats>();
        knockBack = lanStats.KnockBack;
        chr = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    protected void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Mob"))
        {
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), lanStats.Damage);

                // Find a vector from the hero to the enemy
                Vector2 pPos = GameObject.FindGameObjectWithTag("Hero").transform.position;
                Vector2 ePos = c.transform.position;

                // Here is where lances are different
                // Bonus knockback
                //float knockBack = stats.KnockBack;

                // Direction from the player to the mob
                Vector2 lancePos = transform.position;
                Vector3 dir = (lancePos - pPos).normalized;
                Bounds lanceBound = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;

                // Length of the lance attack sprite = y, height = x
                Vector3 bounds = new Vector3((lanceBound.size.y * 1.5f) * dir.x, (lanceBound.size.x / 2f) * dir.y);

                Vector3 lanceTip = transform.position + bounds;
                Vector3 lanceBase = transform.position - bounds;

                float dist = Vector2.Distance(c.transform.position, lanceTip);

                //Debug.Log("Tip " + lanceTip.ToString() + " mob " + c.transform.position.ToString() + " dist " + dist);

                // The damage the attack will deal
                int finalDamage = lanStats.bluntDamage;
                // IF the enemy is at the tip
                if (dist < 0.35f)
                {
                    Debug.Log("Tip'em! " + lanStats.tipDamage);
                    // +1 to the damage
                    finalDamage = lanStats.tipDamage;
                    // +X to the knockback
                    knockBack += 2f;
                }
                // IF the enemy is at the base
                else if (dist > 0.85f)
                {
                    Debug.Log("Basic " + lanStats.baseDamage);
                    // -1 to damage
                    finalDamage = lanStats.baseDamage;
                    // -X to the knockback (but not below zero)
                    knockBack = Mathf.Max(knockBack - 2f, 0);
                }
                // Else 
                // Don't change stats

                Debug.Log("Knockback " + knockBack);
                
                // Calculate the knockback
                vel = (ePos - pPos).normalized * knockBack;

                // Deal the hit
                c.GetComponent<MobController>().Hit(finalDamage, chr, vel);
            }
        }
    }
}
