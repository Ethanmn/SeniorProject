// Make sure it's not killing anything outside the screen

using UnityEngine;

class BottledReaper : Active
{
    public BottledReaper() : base()
    {
        name = "Bottled Reaper";
        sprite = Resources.Load<Sprite>("Sprites/Items/BottledReaper");
        
        // Number of enemies to kill to fully recharge
        maxCharges = 16;
        // Start at max charges
        curCharges = 16;
        // Take all charges to use
        useCharges = 16;
    }

    protected override void ActiveEffect()
    {
        // Kill EVERYTHING (Except bosses)
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        foreach (GameObject mob in mobs)
        {
            mob.GetComponent<MobController>().Hit(999, chr, stats.GetComponent<Transform>().position);
        }
    }
}
