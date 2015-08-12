using System;
using System.Collections.Generic;
using UnityEngine;

class CombustBuff : RuneBuff
{

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onDashPub.RaiseOnDashEvent += HandleOnDashEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onDashPub.RaiseOnDashEvent -= HandleOnDashEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnDashEvent(object sender, POnDashEventArgs e)
    {
        // Do the effect (Drop rune)
        Debug.Log("Dropping rune at " + "(" + e.Hero.position.x + ", " + e.Hero.position.y + ")");
    }

}
