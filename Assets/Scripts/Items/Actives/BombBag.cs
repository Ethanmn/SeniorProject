// Make sure it's not killing anything outside the screen

using UnityEngine;

class BombBag : Active
{
    private float bombDistance = 1f;

    public BombBag() : base()
    {
        name = "Bomb Bag";
        sprite = Resources.Load<Sprite>("Sprites/Items/Bomb");
        
        // Number of enemies to kill to fully recharge
        maxCharges = 6;
        // Start at max charges
        curCharges = 6;
        // Take 3 charges to use
        useCharges = 3;
    }

    protected override void ActiveEffect()
    {
        // Spawn a bomb between the player and the cursor

        // Find player position
        Vector2 pPos = hero.position;
        // Find the mouse position
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the position of the bomb
        Vector2 bPos = ((mPos - pPos).normalized * bombDistance) + new Vector2(pPos.x, pPos.y);

        // Create the bomb
        GameObject bomb = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bomb"));
        // Set the position of the bomb
        bomb.transform.position = bPos;
        // Set the bomb to be a child of the room so that it keeps with the state of the room
        bomb.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform);
    }
}
