﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    private string mobStr;
    public string MobStr
    {
        get { return mobStr; }
        set { mobStr = value; }
    }

    public void Spawn()
    {
        GameObject mob = null;

        if (mobStr == "random")
        {
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

    /*
    void Awake()
    {
        GameObject mob = null;
        if (mobStr == "random")
        {
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
        }
        else
        {
            print("No mob string!");
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
    }*/
}
