﻿using UnityEngine;


public class GeminiStats : MobStats
{
    public int gemID;
    public float gemRange;
    public float switchTime;

    private GameObject twin;

    public Transform Twin
    {
        get
        {
            if (twin)
                return twin.transform;
            else
                return null;
        }
    }

    public GameObject findTwin()
    {
        twin = null;
        if (gemID >= 0)
        {
            GameObject[] mobs;
            mobs = GameObject.FindGameObjectsWithTag("Mob");
            int i = 0;
            while (i < mobs.Length && !twin)
            {
                GeminiStats mobStats = mobs[i].gameObject.GetComponent<MobStats>() as GeminiStats;
                if (mobStats != null && mobStats.gemID == gemID && !gameObject.GetInstanceID().Equals(mobs[i].GetInstanceID())) 
                {
                    twin = mobs[i];
                }
                i++;
            }
        }
        return twin;
    }
}