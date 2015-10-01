using UnityEngine;
using System;

public class POnHitEventArgs : EventArgs
{
    public POnHitEventArgs(Transform en, int dmg)
    {
        enemy = en;
        damage = dmg;
    }

    private Transform enemy;
    private int damage;

    public Transform Enemy
    {
        get { return enemy; }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }
}

public class OnHitPublisher
{
    public event EventHandler<POnHitEventArgs> RaiseOnHitEvent;

    // Call this method to start the event
    public void RaiseEvent(Transform en, int dmg)
    {
        Debug.Log("Enemy " + en.GetComponent<MobStats>().MobName + " was hit!");
        OnRaiseEvent(new POnHitEventArgs(en, dmg));
    }

    protected virtual void OnRaiseEvent(POnHitEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately afer the null check and before the event is raised.

        EventHandler<POnHitEventArgs> handler = RaiseOnHitEvent;

        // Event will be null if there are no subscribers
        if (handler != null)
        {
            // Send out the signal
            handler(this, e);
        }
    }
}
