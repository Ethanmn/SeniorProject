using UnityEngine;
using System.Collections;

public class HeroBlinker : MonoBehaviour {

    private SpriteRenderer sp;
    private HeroStats stats;

    // Counters for blinking animation
    private int blinkCount;
    private bool blink;

    // Timer for timing flinch
    private float timer;

    // Layers
    // Intangible layer
    private int heroFlinch = 10;
    // Normal layer
    private int hero = 9;

    // Use this for initialization
    void Start () {
        sp = gameObject.GetComponent<SpriteRenderer>();
        stats = gameObject.GetComponent<HeroStats>();

        blinkCount = 0;
        blink = false;
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    if (stats.Flinching)
        {
            // Dissable collisions
            gameObject.layer = heroFlinch;

            if (blink)
            {
                sp.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                sp.color = new Color(1f, 1f, 1f, stats.Alpha);
            }

            // Make the hero flash when flinching
            if (blinkCount == 4)
            {
                blink = !blink;
                blinkCount = 0;
            }
            else
                blinkCount++;

            timer += Time.deltaTime;
        }

        if (timer >= stats.FlinchTimer)
        {
            // Turn flinching off
            stats.Flinching = false;

            // Enable collisions
            gameObject.layer = hero;

            // Reset this script
            blinkCount = 0;
            blink = false;
            timer = 0;

            // Make sure the hero is visible
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, stats.Alpha);
        }
	}
}
