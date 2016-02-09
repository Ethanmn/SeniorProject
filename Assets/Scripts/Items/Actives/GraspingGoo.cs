// Make sure it's not killing anything outside the screen

using UnityEngine;

class GraspingGoo : Active
{
    private float bombDistance = 1f;

    public GraspingGoo() : base()
    {
        name = "Grasping Goo";
        sprite = Resources.Load<Sprite>("Sprites/Items/GraspingGoo");
        
        // Number of enemies to kill to fully recharge
        maxCharges = 6;
        // Start at max charges
        curCharges = 6;
        // Take 3 charges to use
        useCharges = 6;
    }

    protected override void ActiveEffect()
    {
        // Create the goo
        GameObject goo = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Goo"));
        // Set the position of the goo
        goo.transform.position = hero.position;
        // Set the goo to be a child of the room so that it keeps with the state of the room
        goo.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform);
    }
}
