using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Trinket : Item
{
    // The buff the trinket confers
    protected TrinketBuff trinketBuff;
    public TrinketBuff TrinketBuff
    {
        get { return trinketBuff; }
    }

    // Buff controller
    protected BuffController buffCon;

    public Trinket()
    {
        // Get the hero's buff controller
        buffCon = GameObject.FindGameObjectWithTag("Hero").GetComponent<BuffController>();

    }

    /// <summary>
    /// When a trinket is equiped, it applies its buff
    /// </summary>
    public override void OnEquip()
    {
        buffCon.AddBuff(trinketBuff);
    }

    /// <summary>
    /// When a trinket is unequiped, it removes its buff
    /// </summary>
    public override void OnUnequip()
    {
        buffCon.RemoveBuff(trinketBuff);
    }
}
