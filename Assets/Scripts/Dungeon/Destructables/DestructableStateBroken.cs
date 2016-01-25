using System;
using System.Collections.Generic;
using UnityEngine;

class DestructableStateBroken : I_DestructableState
{
    private Type[] itemsList = 
        { typeof(BottledReaper), typeof(GhostMote), typeof(StoneSkinSalve),
        /*breaks ammo UI: typeof(OrangePotion),*/ typeof(GreenPotion), typeof(RedPotion), typeof(YellowPotion) };

    // Sprite string name
    private string spriteStr = "Sprites/DestructBPH";
    // Base chance for an item to fall out: 3%
    private int baseItemChance = 97;

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
        // Set the broken sprite
        // Animation
        actor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteStr);

        // Turn off collision
        actor.GetComponent<Collider2D>().enabled = false;

        // Did an item drop?
        itemDrop(actor);
        
    }

    void I_ActorState.OnExit(Transform actor)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform actor, float dt)
    {
        return null;
    }

    protected Active itemDrop(Transform trn)
    {
        // What item did they get?
        Active get = null;

        // Chance to drop item
        int chance = UnityEngine.Random.Range(1, 101);

        // Get item find from player
        int itemFind = 0;

        // Did the player make it?
        if (chance + itemFind > baseItemChance)
        {
            // Yes!
            // Get a random item
            int item = UnityEngine.Random.Range(0, itemsList.Length);
            get = (Active)Activator.CreateInstance(itemsList[item], new object[] { });
            get.Drop(trn.position);
        }

        // Return no item
        return get;
    }
}
