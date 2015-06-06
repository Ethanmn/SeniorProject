using UnityEngine;
using System.Collections;

public class PlayerBlinker : MonoBehaviour {

    private SpriteRenderer sp;
    private PlayerStats stats;

    private int blinkCount;
    private bool blink;

    private float timer;

    // Use this for initialization
    void Start () {
        sp = gameObject.GetComponent<SpriteRenderer>();
        stats = gameObject.GetComponent<PlayerStats>();

        blinkCount = 0;
        blink = false;
        timer = stats.FlinchTimer;
    }
	
	// Update is called once per frame
	void Update () {
	    if (stats.Flinching)
        {
            // Dissable collisions
            gameObject.layer = 10;

            if (blink)
            {
                sp.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                sp.color = new Color(1f, 1f, 1f, stats.Alpha);
            }

            // Make the player flash when flinching
            if (blinkCount == 4)
            {
                blink = !blink;
                blinkCount = 0;
            }
            else
                blinkCount++;

            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            // Turn flinching off
            stats.Flinching = false;

            // Enable collisions
            gameObject.layer = 9;

            // Reset this script
            blinkCount = 0;
            blink = false;
            timer = stats.FlinchTimer;

            // Make sure the player is visible
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, stats.Alpha);
        }
	}
}
