// Only works for gun right now. Need to find a way to make it dynamic

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
        if (++attackCount >= countScale - level)
        {
            // Need a way to find out the velocity (ranged) or position (melee) and oppositeize it

            // Calculate velocity stuff
            Vector2 pPos = e.Hero.GetComponent<Rigidbody2D>().position;
            Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vel = (mPos - pPos).normalized * 15.0f;

            GameObject b = GameObject.Instantiate(e.Attack.gameObject, e.Attack.transform.position, Quaternion.identity) as GameObject;
            b.gameObject.GetComponent<Rigidbody2D>().velocity = -vel;
            b.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BulletPH")[0];

            Debug.Log("Attacking for " + stats.Damage);
            //b.gameObject.GetComponent<RangedAttack>().Damage = stats.Damage;

            // Reset the attack count
            attackCount = 0;
        }
    }

}
