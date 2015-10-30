using UnityEngine;
using System.Collections;

public class SpawnBlob : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject mob = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Blob"));
        if (mob != null)
        {
            // Set the initial position
            mob.transform.parent = gameObject.transform;
            mob.transform.localPosition = Vector3.zero;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
