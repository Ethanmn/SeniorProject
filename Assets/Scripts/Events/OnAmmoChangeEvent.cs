using UnityEngine;
using System;
using System.Collections.Generic;

public class POnAmmoChangeEventArgs : EventArgs
{
    public POnAmmoChangeEventArgs(Transform pl)
    {
        this.hero = pl;
    }

    private Transform hero;

    public Transform Hero
    {
        get { return hero; }
    }
}

public class OnAmmoChangePublisher
{
    public event EventHandler<POnAmmoChangeEventArgs> RaiseOnAmmoChangeEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl)
    {
        OnRaiseEvent(new POnAmmoChangeEventArgs(pl));
    }

    protected virtual void OnRaiseEvent(POnAmmoChangeEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnAmmoChangeEventArgs> handler = RaiseOnAmmoChangeEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
