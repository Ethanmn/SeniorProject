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
    }

    private void initTwins()
    {
        gemStats = gameObject.GetComponent<GeminiStats>();
        if (!gemStats.findTwin())
        {
            Debug.Log("Can't find my twin!");
        }
    }
}
