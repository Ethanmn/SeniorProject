using UnityEngine;
using System.Collections;
using System;

public class TetherScript : MonoBehaviour {

    private TetherStats stats;
    // Collider of the tether
    protected Collider2D col;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<TetherStats>();

        col = GetComponent<Collider2D>();

        // Subscribe to the OnKillEvent to recharge
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
    }
	
	// Update is called once per frame
	void Update () {
        // Find the hero
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");

        if (col.IsTouching(hero.GetComponent<Collider2D>()))
        {
            Debug.Log("Player Tether!");
            // Give the player the buff
            hero.GetComponent<BuffController>().AddBuff(Activator.CreateInstance(stats.TetherBuff) as Buff);
        }
        // Check if you can find the master AND the target
        else if(stats.Master && stats.Target)
        {
            // Add the buff to the target
            stats.Target.GetComponent<BuffController>().AddBuff(Activator.CreateInstance(stats.TetherBuff) as Buff);
        }
        else
        {
            // Kill yourself
            Destroy(gameObject);
        }

        // Calculate the length of tether
        gameObject.transform.localScale = new Vector3(CalculateLength(), 1, 1);

        // Calculate the angle from the master to the target
        gameObject.transform.rotation = CalculateAngle();

        // Calculate the position
        gameObject.transform.position = CalculatePosition();
    }

    private float CalculateLength()
    {
        // Get target position
        Vector3 tarPos = stats.Target.transform.position;
        // Get master position
        Vector3 masPos = stats.Master.transform.position;

        // Distance formula!
        float dist = Mathf.Sqrt(Mathf.Pow((masPos.x - tarPos.x), 2) + Mathf.Pow((masPos.y - tarPos.y), 2));

        // Divide by the length of the sprite
        return dist / gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    private Quaternion CalculateAngle()
    {
        // Get target position
        Vector3 tarPos = stats.Target.transform.position;
        // Get master position
        Vector3 masPos = stats.Master.transform.position;

        Vector3 dir = (tarPos - masPos).normalized;
        
        return Quaternion.FromToRotation(Vector3.right, dir);
    }

    private Vector3 CalculatePosition()
    {
        // Get target position
        Vector3 tarPos = stats.Target.transform.position;
        // Get master position
        Vector3 masPos = stats.Master.transform.position;

        // Find the midpoint
        return new Vector3((tarPos.x + masPos.x) / 2, (tarPos.y + masPos.y) / 2);
    }

    // Handles responding to OnKillEvents
    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        if (e.Mob.Equals(stats.Target) || e.Mob.Equals(stats.Master))
        {
            // Kill yourself
            Destroy(gameObject);
        }
    }
}
