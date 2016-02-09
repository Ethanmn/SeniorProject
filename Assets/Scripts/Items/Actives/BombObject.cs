using UnityEngine;
using System.Collections;

public class BombObject : MonoBehaviour {

    // Num seconds it takes for the bomb to explode
    private float bombTime = 1.0f;
    private float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If it's not time
	    if (bombTime > 0)
        {
            // Keep ticking
            bombTime -= Time.deltaTime;
        }
        else
        {
            // EXPLODE
            GameObject explosion = Instantiate(Resources.Load<GameObject>("Prefabs/OrbExplosion"), transform.position, Quaternion.identity) as GameObject;
            explosion.GetComponent<ExplosionAttack>().damage = 2;
            Destroy(gameObject);
        }
	}
}
