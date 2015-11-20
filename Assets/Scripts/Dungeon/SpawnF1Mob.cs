using UnityEngine;
using System.Collections;

public class SpawnF1Mob : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
    }

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
            default:
                break;
        }

        if (mob != null)
        {
            GameObject spawn = Instantiate(mob);
            // Set the initial position
            spawn.transform.parent = gameObject.transform;
            spawn.transform.localPosition = Vector3.zero;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
