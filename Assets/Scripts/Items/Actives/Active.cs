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

    /// <summary>
    /// Maximum charges the item can hold
    /// </summary>
    public int MaxCharges
    {
        get { return maxCharges; }
    }
    
    /// <summary>
    /// Number charges the item is currently holding
    /// </summary>
    public int CurrentCharges
    {
        get { return curCharges; }
    }

    /// <summary>
    /// Number of charges needed to use the item
    /// </summary>
    public int UseCharges
    {
        get { return useCharges; }
    }

    // Hero stats
    protected HeroStats stats;
    // Hero controller
    protected HeroController control;

    public Active()
    {
        // Standard number of charges to gain is 1
        // Some others may change
        gainCharges = 1;
    }

    public override void OnEquip(Transform chr)
    {
        // Subscribe to the OnKillEvent to recharge
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;

        // Set the hero controller
        control = chr.GetComponent<HeroController>();

        // Set the hero stats variable
        stats = chr.GetComponent<HeroStats>();

        // Set the transform
        this.chr = chr;

        // Signal the event
        //Debug.Log("Signaling");
        PublisherBox.onEquipActivePub.RaiseEvent(chr);
    }

    public override void OnUnequip()
    {
        // Unsubscribe to the OnKillEvent to recharge
        PublisherBox.onKillPub.RaiseOnKillEvent -= HandleOnKillEvent;
    }

    // Check if you can use the active
    /// <summary>
    /// Attempts to use the active. If the item has enough charges, the effect will happen.
    /// </summary>
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

    // Handles responding to OnKillEvents
    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        AddCharges();
    }

    public override void OnDestroy()
    {
        OnUnequip();
    }
}
