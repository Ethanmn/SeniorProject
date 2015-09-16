// This event triggers when the player dies

using UnityEngine;
using System;

public class POnDeathEventArgs : EventArgs
{
    public POnDeathEventArgs(Transform pl)
    {
        this.hero = pl;
    }

    private Transform hero;

    public Transform Hero
    {
        get { return hero; }
    }
}

public class OnDeathPublisher
{
    public event EventHandler<POnDeathEventArgs> RaiseOnDeathEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl)
    {
        OnRaiseEvent(new POnDeathEventArgs(pl));
    }

    protected virtual void OnRaiseEvent(POnDeathEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnDeathEventArgs> handler = RaiseOnDeathEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
