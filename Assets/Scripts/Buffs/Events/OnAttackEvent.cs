// STILL NEEDS TO BE ACTIVATED ON ATTACK
// Need info on the attack itself?

using UnityEngine;
using System;
using System.Collections.Generic;

public class POnAttackEventArgs : EventArgs
{
    public POnAttackEventArgs(Transform hr, Transform atk)
    {
        this.hero = hr;
        this.attack = atk;
    }

    private Transform hero;
    private Transform attack;

    public Transform Hero
    {
        get { return hero; }
    }

    public Transform Attack
    {
        get
        {
            return attack;
        }
    }
}

public class OnAttackPublisher
{
    public event EventHandler<POnAttackEventArgs> RaiseOnAttackEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl, Transform atk)
    {
        OnRaiseEvent(new POnAttackEventArgs(pl, atk));
    }

    protected virtual void OnRaiseEvent(POnAttackEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnAttackEventArgs> handler = RaiseOnAttackEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
