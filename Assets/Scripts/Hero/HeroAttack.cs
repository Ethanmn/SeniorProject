using UnityEngine;
using System;

public class HeroAttack : MonoBehaviour {

    // State of the hero
    private Type state;
    // Stats of the the hero
    private HeroStats stats;
    // The weapon the player is wielding
    public Weapon weapon;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<HeroStats>();

        weapon = new Lance(transform);
        weapon.OnEquip();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Update the weapon
        weapon.Update();

        // Check left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            state = gameObject.GetComponent<HeroController>().GetState().GetType();

            if (!state.Equals(typeof(HeroStateDash)) &&
                !state.Equals(typeof(HeroStateFlinch)))
            {
                weapon.OnMouseDown(gameObject.transform);
            }
        }
        // Check left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            state = gameObject.GetComponent<HeroController>().GetState().GetType();

            if (!state.Equals(typeof(HeroStateDash)) &&
                !state.Equals(typeof(HeroStateFlinch)))
            {
                weapon.OnMouseUp(gameObject.transform);
            }
        }

    }


}
