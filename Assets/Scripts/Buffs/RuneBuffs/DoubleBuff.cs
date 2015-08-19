// Needs a way to know exactly how the weapon builds its attack and then reverse it...

using UnityEngine;

class DoubleBuff : RuneBuff
{
    // How many count per level (how many attacks until effect)
    int countScale = 5;
    // The count of attacks the player has made
    int attackCount = 0;

    public override void OnBegin(Transform chr)
    {
        base.OnBegin(chr);

        // Subscribe to the event using C# 2.0 syntax
        PublisherBox.onAttackPub.RaiseOnAttackEvent += HandleOnAttackEvent;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Unsubscribe when the buff is removed
        PublisherBox.onAttackPub.RaiseOnAttackEvent -= HandleOnAttackEvent;
    }

    // Define what actions to take when event is raised
    private void HandleOnAttackEvent(object sender, POnAttackEventArgs e)
    {
        Debug.Log("Attacks " + attackCount);
        // Do the effect (Double attack)

        // IF the buff flag is set, unset it
        if (stats.DoubleAttack || stats.QuadAttack)
        {
            stats.DoubleAttack = false;
            stats.QuadAttack = false;
        }

        // IF attackCount is greater than or equal to the count scale - level value
        // Increment attackCount
        if (++attackCount >= countScale - level)
        {
            if (level <= 2 && level > 0)
            {
                stats.DoubleAttack = true;
            }
            if (level >= 3)
            {
                stats.QuadAttack = true;
            }
            attackCount = 0;
        }
    }

}
