﻿using UnityEngine;

public class SlowDebuff : Buff
{
    // Hero controller
    private ActorController cont;

    // Timer for debuff duration
    private float timer = 0;
    private float time = 1f;

    // Keep track of the amount slowed
    private float slowAmount = -1f;
    private float slowTrack = 0;

    public override void OnBegin(Transform character)
    {
        chr = character;
        cont = character.GetComponent<ActorController>();
        timer = time;
    }

    public override void OnEnd()
    {
        // IF the slow is still applied (it should be)
        if (slowTrack == slowAmount)
        {
            if (chr.CompareTag("Mob"))
            {
                chr.GetComponent<MobStats>().BonusSpeed -= slowAmount;
            }
            else if (chr.CompareTag("Hero"))
            {
                chr.GetComponent<HeroStats>().BonusSpeed -= slowAmount;
            }
            slowTrack = 0;
        }
    }

    public override void OnUpdate()
    {
        // IF the slow amount hasn't been applied yet
        if (slowTrack != slowAmount)
        {
            // Add the slow
            if (chr.CompareTag("Mob"))
            {
                chr.GetComponent<MobStats>().BonusSpeed += slowAmount;
            }
            else if (chr.CompareTag("Hero"))
            {
                chr.GetComponent<HeroStats>().BonusSpeed += slowAmount;
            }

            slowTrack = slowAmount;
        }
        // IF the burn has not run out
        if (timer > 0)
        {
            // Count up
            timer -= Time.deltaTime;
        }
        // If the ticks have run out
        if (timer <= 0)
        {
            // Remove the buff
            remove = true;
        }
    }

    public override void Refresh()
    {
        // Refresh to the max time
        timer = time;
    }
}