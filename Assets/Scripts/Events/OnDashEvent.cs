using UnityEngine;
using System;
using System.Collections.Generic;

public class POnDashEventArgs : EventArgs
{
    public POnDashEventArgs(Transform pl)
    {
        this.hero = pl;
    }

    private Transform hero;

    public Transform Hero
    {
        get { return hero; }
    }
}

public class OnDashPublisher
{
    public event EventHandler<POnDashEventArgs> RaiseOnDashEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl)
    {
        OnRaiseEvent(new POnDashEventArgs(pl));
    }

    protected virtual void OnRaiseEvent(POnDashEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnDashEventArgs> handler = RaiseOnDashEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
