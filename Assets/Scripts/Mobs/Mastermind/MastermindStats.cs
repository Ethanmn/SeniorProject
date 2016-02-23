using UnityEngine;
using System;
using System.Collections.Generic;

public class MastermindStats : MobStats
{
    // Key: Mob || Value: Tether
    private Dictionary<GameObject, GameObject> tethers = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> Tethers
    {
        get { return tethers; }
        set { tethers = value; }
    }

    // List of buffs the tethers can give
    private Type[] buffsList =
        { typeof(DamageBuff), typeof(SpeedBuff), typeof(HealthBuff),
        typeof(DamageDebuff), typeof(SpeedDebuff), typeof(HealthDebuff)};

    public Type[] BuffsList
    {
        get { return buffsList; }
    }

}
