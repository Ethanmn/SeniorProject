﻿// Make sure it's not killing anything outside the screen

using System;
using System.Collections.Generic;
using UnityEngine;

class BottledReaper : Active
{
    public BottledReaper() : base()
    {
        // Number of enemies to kill to fully recharge
        maxCharges = 16;
        // Start at max charges
        curCharges = 16;
        // Take all charges to use
        useCharges = 16;
    }

    public override void OnEquip()
    {
        
    }

    public override void OnUnequip()
    {
        
    }

    protected override void ActiveEffect()
    {
        // Kill EVERYTHING
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        foreach (GameObject mob in mobs)
        {
            mob.GetComponent<MobController>().Hit(999, stats.GetComponent<Transform>().position);
        }
    }
}