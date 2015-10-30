using UnityEngine;
using System.Collections;

public class SpawnF1Mob : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject mob = null;
        int randMob = Random.Range(0, 3);
        print("Rand " + randMob);
        switch(randMob)
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
            default:
                break;
        }
        
        if (mob != null)
        {
            GameObject spawn = Instantiate(mob);
            // Set the initial position
            spawn.transform.parent = gameObject.transform;
            spawn.transform.localPosition = Vector3.zero;
            if (randMob == 2)
            {
                GameObject twin = Instantiate(mob);
                // Set the initial position
                twin.transform.parent = gameObject.transform;
                twin.transform.localPosition = new Vector3(0, -1.0f, 0);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
