using UnityEngine;
using System.Collections;

public class DestructableStats : MonoBehaviour
{
    // Health of a destructable
    public int maxHealth;

    // Current state the destructable is in
    public int curHealth;

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
