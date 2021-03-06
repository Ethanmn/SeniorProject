﻿using UnityEngine;

public abstract class Buff
{
    // Flag the buff as needing to be removed
    protected bool remove = false;

    /// <summary>
    /// Accessor to remove flag
    /// </summary>
    public bool Remove
    {
        get { return remove; }
        set { remove = value; }
    }

    // Buff's name / key
    protected string buffName;

    /// <summary>
    /// Name of the buff.
    /// </summary>
    public string BuffName
    {
        get
        {
            return buffName;
        }
    }

    // The field that holds the transform of the character the buff is on
    protected Transform chr;
    // Property for the buff's character (who the buff controller is on)
    public Transform Chr { get { return chr; } }

    /// <summary>
    /// The method called when the buff is first applied.
    /// </summary>
    /// <param name="character">Transform of the character the buff is applied to.</param>
    abstract public void OnBegin(Transform character);

    /// <summary>
    /// Method to be run when a buff expires or is removed.
    /// </summary>
    abstract public void OnEnd();

    /// <summary>
    /// Method to be run on a buff every update frame.
    /// </summary>
    abstract public void OnUpdate();

    /// <summary>
    /// Method to be run when a buff already exists in the buff controller and is being added again.
    /// Generall used for rune buffs to add a stack, or to refresh buff timers
    /// </summary>
    abstract public void Refresh();

    // Constructor
    public Buff()
    {
        buffName = GetType().Name;
    }
}
