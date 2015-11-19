using UnityEngine;


public class GeminiStats : MobStats
{
    public int gemID;
    public float gemRange;
    public float switchTime;

    public bool original = true;

    // Incapacitated, not dead
    public bool incap = false;

    // New health to manage incap vs death
    /*
    public override int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                // Not dead, just incapacitated
                incap = true;
            }
            else
            {
                incap = false;
            }
        }
    }*/

    private float colorVariance;
    public float ColorVariance
    {
        get { return colorVariance; }
    }

    private GameObject twin;

    void Awake()
    {
        colorVariance = Random.Range(0.2f, 1);
    }

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
