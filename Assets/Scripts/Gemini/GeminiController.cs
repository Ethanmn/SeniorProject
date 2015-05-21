using UnityEngine;
using System.Collections;

public class GeminiController : MobController
{

    public GeminiController()
    {
        startState = new GeminiStateIdle();
        //flinchState = new GeminiStateFlinch(Vector2.zero);
        //deathState = new GeminiStateDeath();
    }

    public override void Update()
    {
        // IF Gemini is low on heath AND Twin has HIGHER health, switch places
        // ELSE do your usual thang
        base.Update();
    }
}
