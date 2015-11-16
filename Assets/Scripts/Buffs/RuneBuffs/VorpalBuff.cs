using UnityEngine;

class VorpalBuff : RuneBuff
{
    // Timer on which the levels will scale
    private int timerScale = 8;
    // Timer for timing when the buff will happen
    private float timer = 0;
    // Flag for when the crit has been set for this buff
    private bool crit = false;
    // Muliplier to be added to stats
    private int multiplier;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Initialize the timer
        timer = 0;

        // Subscribe to the OnAttackEvent
        PublisherBox.onAttackPub.RaiseOnAttackEvent += HandleOnAttackEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe to the OnAttackEvent when removed
        PublisherBox.onAttackPub.RaiseOnAttackEvent -= HandleOnAttackEvent;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!crit)
        {
            timer += Time.deltaTime;
        }

        if ((int) timer == timerScale - (2 * (level-1)))
        {
            // Determin the size of the mulitplier
            // Levels 1 & 2 double damage
            if (level <= 2)
            {
                multiplier = 2;
            }
            // Level 3 triples damage
            else if (level >= 3)
            {
                multiplier = 3;
            }
            else
            {
                multiplier = 0;
            }

            

            // Add the multiplier to the stats
            stats.DamageMuliplier += multiplier;

            Debug.Log("CRIT READY! " + stats.DamageMuliplier);

            crit = true;
            timer = 0;
        }
    }

    // Define what actions to take when event is raised
    private void HandleOnAttackEvent(object sender, POnAttackEventArgs e)
    {
        // The crit is used up on attack weather or not it hits
        if (crit)
        {
            crit = false;
            stats.DamageMuliplier -= multiplier;
        }
    }
}

