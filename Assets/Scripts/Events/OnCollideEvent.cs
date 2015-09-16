using UnityEngine;
using System;
using System.Collections.Generic;

public class POnCollideEventArgs : EventArgs
{
    public POnCollideEventArgs(Transform hr, Transform en)
    {
        enemy = en;
        hero = hr;
    }

    private Transform enemy;
    private Transform hero;

    public Transform Enemy
    {
        get { return enemy; }
    }

    public Transform Hero
    {
        get
        {
            return hero;
        }
    }
}

public class OnCollidePublisher
{
    public event EventHandler<POnCollideEventArgs> RaiseOnCollideEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform pl, Transform en)
    {
        // Debug.Log("Enemy " + en.GetComponent<MobStats>().mobName + " was hit!");
        OnRaiseEvent(new POnCollideEventArgs(pl, en));
    }

    protected virtual void OnRaiseEvent(POnCollideEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnCollideEventArgs> handler = RaiseOnCollideEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
