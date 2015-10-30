using UnityEngine;

public class MamaBlobStateDeath : I_MobState
{
    private float timer;
    private int blinkCount;
    private bool blink;
    private MamaBlobStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        // Get the stats of the mob
        stats = mob.GetComponent<MobStats>() as MamaBlobStats;

        timer = stats.deathTimer;
        blinkCount = 0;
        blink = false;
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void I_ActorState.OnExit(Transform mob)
    {

    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (blink)
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (timer <= 0)
        {
            if (stats == null)
            {
                Debug.Log("NO stats!");
            }
            // These should probably aggro to the player
            for (int i = 0; i < stats.numBabies; i++)
            {
                GameObject baby = GameObject.Instantiate(Resources.Load("Prefabs/Blob")) as GameObject;
                baby.gameObject.GetComponent<Transform>().position = mob.position + new Vector3(
                    Random.Range(-stats.spawnRange, stats.spawnRange), Random.Range(-stats.spawnRange, stats.spawnRange), 0f);
                baby.transform.parent = mob;
            }

            //GameObject.Destroy(mob.gameObject);
            // Turn off the collider and renderer
            mob.GetComponent<SpriteRenderer>().enabled = false;
            mob.GetComponent<CircleCollider2D>().enabled = false;
            mob.GetComponent<MobController>().enabled = false;
        }

        if (blinkCount == 4)
        {
            blink = !blink;
            blinkCount = 0;
        }
        else
            blinkCount++;

        timer -= dt;
        return null;
    }
    I_MobState I_MobState.FixedUpdate(Transform mob, float dt)
    {
        return null;
    }

    I_ActorState I_ActorState.HandleInput(Transform mob)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionStay(Transform mob, Collision2D c)
    {
        return null;
    }

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
