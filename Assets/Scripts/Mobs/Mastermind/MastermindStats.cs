using UnityEngine;
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
}
