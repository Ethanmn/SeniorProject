﻿using System;
using UnityEngine;

class GhostMoteBuff : Buff
{
    // Duration of the buff
    private float timer;
    // Bonus damage given by buff
    private int flTime = 3;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();
        // Add bonus flinch time
        stats.BonusFlinchTime += flTime;
        // Reset the timer
        timer = stats.FlinchTimer;
    }

    public override void OnEnd()
    {
        // Remove bonus flinch time
        stats.BonusFlinchTime -= flTime;
    }

    public override void OnUpdate()
    {
        if (timer <= 0)
        {
            remove = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public override void Refresh()
    {
        timer = flTime;
    }
}

