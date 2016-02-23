using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public string mobStr;
    public string MobStr
    {
        get { return mobStr; }
        set { mobStr = value; }
    }

    private string[] mobs =
        {"Blob", "MamaBlob", "Gemini", "Mastermind", "Spark" };

    public void Spawn()
    {
        GameObject mob = null;

        if (mobStr.ToLower() == "random")
        {
            int randMob = Random.Range(0, mobs.Length);
            mob = Resources.Load<GameObject>("Prefabs/" + mobs[randMob]);
        }
        else
        {
            mob = Resources.Load<GameObject>("Prefabs/" + mobStr);
        }

        if (mob != null)
        {
            GameObject spawn = Instantiate(mob);
            // Set the initial position
            spawn.transform.parent = gameObject.transform;
            // Get the sprite rendered to find the size and move the position
            SpriteRenderer spawnSR = spawn.GetComponent<SpriteRenderer>();
            spawn.transform.localPosition = new Vector3(spawnSR.bounds.size.x / 2, spawnSR.bounds.size.y / 2, 0);
        }
    }

    void Awake()
    {
        Spawn();
    }
}
