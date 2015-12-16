using UnityEngine;
using System.Collections;

public class GeminiController : MobController
{
    Transform twin;
    GeminiStats gemStats;

    public GeminiController()
    {
        startState = new GeminiStateIdle();
        flinchState = new GeminiStateFlinch(Vector2.zero);
        deathState = new GeminiStateDeath();
    }

    public override void Start()
    {
        initTwins();
        base.Start();

        float colorVariance = gameObject.GetComponent<GeminiStats>().ColorVariance;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(colorVariance, 1f, 1f);
    }

    private void initTwins()
    {
        gemStats = gameObject.GetComponent<GeminiStats>();

        if (gemStats.original)
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
            foreach (GameObject mob in mobs)
            {
                if (!mob.Equals(gameObject) &&
                    mob.GetComponent<GeminiStats>() != null &&
                    mob.GetComponent<GeminiStats>().original &&
                    mob.GetComponent<GeminiStats>().gemID == gemStats.gemID)
                {
                    gemStats.gemID++;
                }
            }

            GameObject spawn = Instantiate(Resources.Load<GameObject>("Prefabs/Gemini"));
            // Set the initial position
            spawn.transform.parent = gameObject.transform.parent;
            spawn.transform.position = gameObject.transform.position + new Vector3(0.5f, 0);
            spawn.GetComponent<GeminiStats>().original = false;
            spawn.GetComponent<GeminiStats>().gemID = gemStats.gemID;
        }

        if (!gemStats.findTwin())
        {
            Debug.Log("Can't find my twin!");
        }
    }
}
