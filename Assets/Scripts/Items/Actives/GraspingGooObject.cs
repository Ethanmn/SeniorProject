using UnityEngine;
using System.Collections;

public class GraspingGooObject : MonoBehaviour {

    // Num seconds it takes for the bomb to explode
    private float gooTime = 5.0f;
    private float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If it's not time
	    if (gooTime > 0)
        {
            // Keep ticking
            gooTime -= Time.deltaTime;
        }
        else
        {
            // Time's up!
            Destroy(gameObject);
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        // If it's a mob
        if (col.gameObject.CompareTag("Mob"))
        {
            Debug.Log("Slowing!");
            // Slow it
            col.GetComponent<BuffController>().AddBuff(new SlowDebuff());
        }
    }
}
