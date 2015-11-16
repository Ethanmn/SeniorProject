using UnityEngine;

public abstract class Active : Item
{
    // Max number of charges
    protected int maxCharges;
    // Number of charges required to use Active
    protected int useCharges;
    // Current number of charges
    protected int curCharges;
    // Number of charges gained per kill
    protected int gainCharges;

    // Active flag (So items on the floor do not recharge)
    protected bool equipped;

    // Hero stats
    protected HeroStats stats;
    // Hero controller
    protected HeroController control;

    public Active()
    {
        // Standard number of charges to gain is 1
        // Some others may change
        gainCharges = 1;

        equipped = false;

        // Subscribe to the OnKillEvent to recharge
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
    }

    public override void OnEquip(Transform chr)
    {
        // Set the hero controller
        control = chr.GetComponent<HeroController>();

        // Set the hero stats variable
        stats = chr.GetComponent<HeroStats>();

        // Set the transform
        this.chr = chr;

        // Activate the active item
        equipped = true;
    }

    public override void OnUnequip()
    {
        equipped = false;
    }

    // Check if you can use the active
    public void UseActive()
    {
        if (curCharges >= useCharges)
        {
            curCharges -= useCharges;
            ActiveEffect();

            if (stats.Antiquarian)
            {
                AddCharges();
                AddCharges();
            }
        }
        else
        {
            Debug.Log("Not enough charges!");
        }
    }

    // The action the active will do when used
    // To be overwritten by specific Active Items
    protected abstract void ActiveEffect();

    // Add charges
    private void AddCharges()
    {
        if (equipped)
        {
            if (curCharges < maxCharges)
            {
                if (curCharges + gainCharges >= maxCharges)
                {
                    curCharges = maxCharges;
                }
                else
                {
                    curCharges += gainCharges;
                }
            }

            Debug.Log(name + " Charges " + curCharges);
        }
    }

    // Handles responding to OnKillEvents
    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        AddCharges();
    }
}
