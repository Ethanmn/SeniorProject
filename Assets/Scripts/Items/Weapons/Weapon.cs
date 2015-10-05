// Add a stat to reduce swing timer

using System;
using UnityEngine;

public abstract class Weapon : Item
{
    // Speed at which the hero can attack
    protected float swingTime;
    // Timer to time swings
    protected float swingTimer;

    // Base damage the weapon deals
    protected int damage;

    // The knockback value
    protected float knockback;

    // The attack GameObject that actually deals damage
    protected GameObject attack;

    // Stats of the hero equiped with the weapon
    protected HeroStats stats;

    public Weapon(Transform hero)
    {
        stats = hero.GetComponent<HeroStats>();
        swingTimer = 0;
        //swingTime -= stats.SwingTimeReduction
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    public virtual void OnMouseDown(Transform hero)
    {

    }

    public virtual void OnMouseUp(Transform hero)
    {
        // The swing is ready
        if (swingTimer <= 0)
        {
            Attack(hero);
            // Reset the swing timer
            swingTimer = swingTime * stats.BonusSwingTimeMultiplier;
        }
    }

    protected virtual void Attack(Transform hero)
    {
        // Raise the event that signals an attack
        // As per event convention, should be called AFTER specific
        //   weapon functions
        PublisherBox.onAttackPub.RaiseEvent(hero, attack.transform);
    }

    public void Update()
    {
        // If the swing is not ready
        if (swingTimer > 0)
        {
            // Lower the timer
            swingTimer -= Time.deltaTime;
        }
    }

    public override void OnEquip(Transform chr)
    {
        // Add the damage from the weapon to the stats
        stats.Damage = damage;
    }

    public override void OnUnequip()
    {
        // Add the damage from the weapon to the stats
        stats.Damage = 0;
    }


}
