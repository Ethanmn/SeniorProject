using UnityEngine;
using System;
using System.Collections.Generic;

public class POnHurtEventArgs : EventArgs
{
    public POnHurtEventArgs(Transform pl, Transform en)
    {
        this.hero = pl;
        this.enemy = en;
    }

    private Transform enemy;
    private Transform hero;

    public Transform Enemy
    {
        get { return enemy; }
    }

    public Transform Hero
    {
        get { return hero; }
    }
}

public class OnHurtPublisher
{
    public event EventHandler<POnHurtEventArgs> RaiseOnHurtEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl, Transform en)
    {
        if (en.tag == "Mob")
            Debug.Log("Enemy " + en.GetComponent<MobStats>().mobName + " hit hero!");
        OnRaiseEvent(new POnHurtEventArgs(pl, en));
    }

    protected virtual void OnRaiseEvent(POnHurtEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnHurtEventArgs> handler = RaiseOnHurtEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
