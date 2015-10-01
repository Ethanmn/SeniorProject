using System;
using UnityEngine;

public interface I_ActorState
{
    void OnEnter(Transform actor);
    void OnExit(Transform actor);

    // Update is called once per frame
    I_ActorState Update(Transform actor, float dt);
    I_ActorState HandleInput(Transform actor);
    I_ActorState OnCollisionEnter(Transform actor, Collision2D c);
    I_ActorState OnCollisionStay(Transform actor, Collision2D c);
}