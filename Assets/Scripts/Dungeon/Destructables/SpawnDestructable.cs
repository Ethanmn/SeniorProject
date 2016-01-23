using UnityEngine;
using System.Collections;

public class SpawnDestructable : MonoBehaviour {

    void Awake()
    {
        GameObject spawn = Instantiate(Resources.Load<GameObject>("Prefabs/Crate"));
        // Set the initial position
        spawn.transform.parent = gameObject.transform;
        // Get the sprite rendered to find the size and move the position
        SpriteRenderer spawnSR = spawn.GetComponent<SpriteRenderer>();
        spawn.transform.localPosition = new Vector3(spawnSR.bounds.size.x / 2, spawnSR.bounds.size.y / 2, 0);
    }

}
