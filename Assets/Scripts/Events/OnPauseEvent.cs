using UnityEngine;
using System;
using System.Collections.Generic;

public class POnPauseEventArgs : EventArgs
{
    public POnPauseEventArgs(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    private bool isPaused;
    public bool IsPaused
    {
        get { return isPaused; }
    }
}

public class OnPausePublisher
{
    public event EventHandler<POnPauseEventArgs> RaiseOnPauseEvent;

    // Call this method to start the event
    public void RaiseEvent(bool isPaused)
    {
        OnRaiseEvent(new POnPauseEventArgs(isPaused));
    }

    protected virtual void OnRaiseEvent(POnPauseEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnPauseEventArgs> handler = RaiseOnPauseEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
