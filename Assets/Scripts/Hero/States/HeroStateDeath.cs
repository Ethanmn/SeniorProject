using UnityEngine;

public class HeroStateDeath : I_HeroState {

    void I_HeroState.OnEnter(Transform hero)
    {
        
    }

    void I_HeroState.OnExit(Transform hero)
    {
        
    }

    I_HeroState I_HeroState.Update(Transform hero, float dt)
    {
        GameObject.Destroy(hero.gameObject);
        return null;
    }

    I_HeroState I_HeroState.HandleInput(Transform hero)
    {
        return null;
    }

    I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
    {
        return null;
    }

}
