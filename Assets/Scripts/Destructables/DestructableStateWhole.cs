using System;
using System.Collections.Generic;
using UnityEngine;

class DestructableStateWhole : I_DestructableState
{
    string spriteStr = "Sprites/DestructWPH";

    I_ActorState I_ActorState.HandleInput(Transform actor)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionStay(Transform actor, Collision2D c)
    {
        return null;
    }

    void I_ActorState.OnEnter(Transform actor)
    {
        actor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteStr);
    }

    void I_ActorState.OnExit(Transform actor)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform actor, float dt)
    {
        return null;
    }
}
