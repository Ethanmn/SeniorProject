using UnityEngine;

public class HeroStatePause : I_HeroState {

    void I_ActorState.OnEnter(Transform hero)
    {
        
    }

    void I_ActorState.OnExit(Transform hero)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform hero, float dt)
    {
        return null;
    }

    I_ActorState I_ActorState.HandleInput(Transform hero)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionEnter(Transform hero, Collision2D c)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionStay(Transform actor, Collision2D c)
    {
        return null;
    }

}
