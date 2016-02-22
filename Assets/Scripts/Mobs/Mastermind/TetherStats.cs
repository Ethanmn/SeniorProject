using System;
using UnityEngine;

public class TetherStats : MonoBehaviour {

    // Mastermind tether originates from
    private GameObject master;
    public GameObject Master
    {
        get { return master; }
        set { master = value; }
    }

    // Target tether buffs
    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    // What buff does this tether apply?
    private Type tetherBuff;
    public Type TetherBuff
    {
        get { return tetherBuff; }
        set { tetherBuff = value; }
    }
}
