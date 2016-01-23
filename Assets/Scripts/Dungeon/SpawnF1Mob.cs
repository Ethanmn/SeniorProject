using UnityEngine;
using System.Collections;

public class SpawnF1Mob : MonoBehaviour {

    void Awake()
    {
        GameObject mob = null;
        int randMob = Random.Range(0, 4);
        switch (randMob)
        {
            case 0:
                mob = Resources.Load<GameObject>("Prefabs/Blob");
                break;
            case 1:
                mob = Resources.Load<GameObject>("Prefabs/MamaBlob");
                break;
            case 2:
                mob = Resources.Load<GameObject>("Prefabs/Gemini");
                break;
            case 3:
                mob = Resources.Load<GameObject>("Prefabs/Spark");
                break;
            default:
                break;
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
}
