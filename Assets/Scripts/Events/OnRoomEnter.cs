using UnityEngine;
using System;

public class POnRoomEnterEventArgs : EventArgs
{
    public POnRoomEnterEventArgs(int floor, int x, int y)
    {
        this.floor = floor;
        this.x = x;
        this.y = y;
    }

    private int floor;
    private int x;
    private int y;

    public int Floor
    {
        get { return floor; }
    }
    public int X
    {
        get { return x; }
    }
    public int Y
    {
        get { return y; }
    }
}

public class OnRoomEnterPublisher
{
    public event EventHandler<POnRoomEnterEventArgs> RaiseOnRoomEnterEvent;

    // Call this method to start the event
    /// <summary>
    /// Raises an event for the room indicated by (x, y)
    /// </summary>
    /// <param name="floor">Floor on which the room exists.</param>
    /// <param name="x">X coordinate of the room.</param>
    /// <param name="y">Y coordinate of the room.</param>
    public void RaiseEvent(int floor, int x, int y)
    {
        OnRaiseEvent(new POnRoomEnterEventArgs(floor, x, y));
    }

    protected virtual void OnRaiseEvent(POnRoomEnterEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnRoomEnterEventArgs> handler = RaiseOnRoomEnterEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
