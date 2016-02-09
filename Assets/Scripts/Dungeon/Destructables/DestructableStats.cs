using UnityEngine;
using System.Collections;

public class DestructableStats : MonoBehaviour
{
    // Health of a destructable
    public int maxHealth;

    // Current state the destructable is in
    public int curHealth;

    // Base chance for an item to fall out: 10%
    public int baseItemChance = 90;

    ///////////////////
    // Public accessors
    ///////////////////

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int CurHealth
    {
        get { return curHealth; }
        set
        {
            curHealth = value;
        }
    }
}
