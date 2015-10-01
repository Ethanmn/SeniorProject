// NOTE: Each card should have its own item sprite to show when used

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class DeckOfFates : Active
{
    // Number of cards in the deck
    int numCards = 12;

    // List numbers representing cards
    private List<int> cards;

    // Body Card heal
    private int heal = 5;

    // Heartbreak Card damage
    private int heartBreak = 3;

    // Passion burn time
    private int burn = 2;

    public DeckOfFates() : base()
    {
        // Number of enemies to kill to fully recharge
        maxCharges = 8;
        // Start at max charges
        curCharges = 8;
        // Take all charges to use
        useCharges = 0;

        cards = Enumerable.Range(0, numCards).ToList();
    }

    public override void OnEquip()
    {
        
    }

    public override void OnUnequip()
    {
        
    }

    protected override void ActiveEffect()
    {
        // IF there are cards left in the deck
        if (cards.Count > 0)
        {
            // Seed the random
            Random.seed = (int)System.DateTime.Now.Ticks;
            // Pick a random effect
            int card = 10;//Random.Range(0, cards.Count);

            // Run that effect
            // Set the corresponding card sprite
            switch (cards[card])
            {
                // Body Card
                case 0:
                    Debug.Log("Body Card");
                    // Heal
                    control.Heal(heal);
                    break;
                // Dust Card
                case 1:
                    Debug.Log("Dust Card");
                    // Summon dust
                    break;
                // Hero Card
                case 2:
                    Debug.Log("Hero Card");
                    // Become invulnerable for flinch + 6
                    // Add buff to extend flinch time
                    stats.GetComponent<BuffController>().AddBuff(new HeroCardBuff());
                    // Activate flinch
                    stats.Flinching = true;
                    break;
                // Antique Card
                case 3:
                    Debug.Log("Antique Card");
                    // Summon a square of destructables
                    break;
                // Smite Card
                case 4:
                    Debug.Log("Smite Card");
                    // UNFINISHED: Need to damage bosses as well, and a velocity

                    // Vector for velocity at which to hit enemies
                    Vector2 vel;
                    // Standard knock back value for melee weapons
                    float knockBack = 3f;

                    // Damage all enemies in the room (including bosses)
                    GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
                    foreach (GameObject mob in mobs)
                    {
                        vel = (chr.position - mob.transform.position).normalized * knockBack;
                        mob.GetComponent<MobController>().Hit(10, chr, vel);
                    }
                    GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
                    foreach (GameObject boss in bosses)
                    {
                        vel = (chr.position - boss.transform.position).normalized * knockBack;
                        boss.GetComponent<MobController>().Hit(10, chr, vel);
                    }
                    break;
                // Treasure Card
                case 5:
                    Debug.Log("Treasure Card");
                    // Summon a random trinket
                    break;
                // Crystal Card
                case 6:
                    Debug.Log("Crystal Card");
                    // Summon 3 random rune fragments
                    break;
                // Death Card
                case 7:
                    Debug.Log("Death Card");
                    // Gain +10 damage, 1.0 attack speed, +8 ammo for 5 seconds
                    stats.GetComponent<BuffController>().AddBuff(new DeathCardBuff());
                    break;
                // Teeth Card
                case 8:
                    Debug.Log("Teeth Card");
                    // Random spike traps are created
                    break;
                // Hearthbreak Card
                case 9:
                    Debug.Log("Heartbreak Card");
                    // Lose 3 HP (Cannot kill)
                    // Find the position of the mouse to use when applying damage
                    Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    GameObject pos = new GameObject();
                    pos.transform.position = mPos;

                    // IF the damage would NOT kill, apply the damage
                    if (stats.Health - heartBreak > 0)
                    {
                        stats.GetComponent<HeroController>().Hit(heartBreak, pos.transform, Vector2.zero);
                    }
                    // ELSE if the damage WILL kill, apply damage down to 1 HP
                    else
                    {
                        stats.GetComponent<HeroController>().Hit(stats.Health - 1, pos.transform, Vector2.zero);
                    }
                    Object.Destroy(pos);
                    break;
                // Passion Card
                case 10:
                    Debug.Log("Passion Card");
                    // All units in the room are set on fire (including the player), 1 damage / second for 4 seconds
                    stats.GetComponent<BuffController>().AddBuff(new BurnDebuff(burn));
                    mobs = GameObject.FindGameObjectsWithTag("Mob");

                    foreach (GameObject mob in mobs)
                    {
                        mob.GetComponent<BuffController>().AddBuff(new BurnDebuff(burn));
                    }

                    break;
                // Blank Card
                case 11:
                    Debug.Log("Blank Card");
                    // Pretty much nothing
                    break;
                default:
                    break;
            }

            // Remove the card
            cards.RemoveAt(card);
        }
        // If the deck is out, "shuffle" it (refill)
        else
        {
            Debug.Log("!!!Refilling the deck!!!");
            cards = Enumerable.Range(0, numCards).ToList();
        }

    }
}
