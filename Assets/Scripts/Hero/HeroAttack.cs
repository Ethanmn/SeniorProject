using UnityEngine;
using System;

public class HeroAttack : MonoBehaviour {

    // State of the hero
    private Type state;
    // The weapon the player is wielding
    private Weapon weapon;
    // Inventory of the hero
    private HeroInventory inv;
    // Controller of the hero
    private HeroController controller;

    // Use this for initialization
    void Start () {
        // Get the heirloom from the inventory
        inv = gameObject.GetComponent<HeroInventory>();
        controller = gameObject.GetComponent<HeroController>();

        weapon = null;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get the weapon from the inventory
        if (inv.Heirloom != null)
        {
            weapon = inv.Heirloom.Weapon;
        }

        if (weapon != null)
        {
            // Update the weapon
            weapon.Update();
        }

        state = controller.State.GetType();
        // All input must not happen in these states
        if (!state.Equals(typeof(HeroStateDash)) &&
                !state.Equals(typeof(HeroStateFlinch)) &&
                weapon != null)
        {
            // Check left mouse button is held down
            if (Input.GetMouseButton(0))
            {
                weapon.OnMouseDown(gameObject.transform);
            }
            // Check left mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                weapon.OnMouseUp(gameObject.transform);
            }

            // Check for the reload button
            if (Input.GetKeyUp(KeyCode.R))
            {
                // Reload
                controller.SetState(new HeroStateReload());
            }
        }
    }


}
