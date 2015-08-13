using UnityEngine;
using System.Collections;

public class HeroAttack : MonoBehaviour {

    // State of the hero
    private I_HeroState state;
    // Stats of the the hero
    private HeroStats stats;
    // The weapon the player is wielding
    private Weapon weapon;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<HeroStats>();

        weapon = new Sword(transform);
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
            state = gameObject.GetComponent<HeroController>().GetState();

            if (!state.GetType().Equals(typeof(HeroStateDash)))
            {
                weapon.OnMouseDown(gameObject.transform);
            }
        }
        // Check left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            state = gameObject.GetComponent<HeroController>().GetState();

            if (!state.GetType().Equals(typeof(HeroStateDash)))
            {
                weapon.OnMouseUp(gameObject.transform);
            }
        }

    }


}
