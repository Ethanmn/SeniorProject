using System;
using UnityEngine;

class TestSpeedBuff : Buff
{ 
    private int seconds;
    private float dur;

    private HeroStats stats;

    public override void OnBegin(Transform character)
    {
        Debug.Log("Buff starting!");

        dur = 10f;
        seconds = (int)dur;

        chr = character;
        stats = chr.GetComponent<HeroStats>();
    }

    public override void OnEnd()
    {
        Debug.Log("Buff ending!");

        stats.BonusSpeed -= 20;
    }

    public override void OnUpdate()
    {
        Debug.Log("Buff updating!");

        dur -= Time.deltaTime;

        if (seconds > (int)dur)
        {
            stats.BonusSpeed += 2;
            seconds = (int)dur;
            Debug.Log("Seconds " + seconds);
        }
        
        if (dur <= 0)
        {
            remove = true;
        }
    }
}
