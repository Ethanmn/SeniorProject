// This event triggers when the player dies

using UnityEngine;
using System;

public class POnKillEventArgs : EventArgs
{
    public POnKillEventArgs(Transform mb)
    {
        this.mob = mb;
    }

    private Transform mob;

    public Transform Hero
    {
        get { return mob; }
    }
}

public class OnKillPublisher
{
    public event EventHandler<POnKillEventArgs> RaiseOnKillEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform mb)
    {
        OnRaiseEvent(new POnKillEventArgs(mb));
    }

    protected virtual void OnRaiseEvent(POnKillEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnKillEventArgs> handler = RaiseOnKillEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
