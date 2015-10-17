using UnityEngine;
using System.Collections;

public class HealthChangeTextScript : MonoBehaviour {

    Animator textAnim;

	// Use this for initialization
	void Start () {
        textAnim = gameObject.GetComponent<Animator>();
        // Wait for animation to finish
        StartCoroutine(AnimationYield());
        // Kill this object when the animation is over
        print("Animation over!");
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private IEnumerator AnimationYield()
    {
        yield return new WaitForSeconds(1);
    }
}
