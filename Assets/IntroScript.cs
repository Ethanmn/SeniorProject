using UnityEngine;

public class IntroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero)
        {
            if (hero.GetComponent<HeroStats>().Generation == 0)
            {
                foreach (Transform piece in transform)
                {
                    piece.gameObject.SetActive(true);
                }
            }
            else
                gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("IntroScript cannot find hero!");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartButton()
    {
        // When the button is pushed, turn off the intro screen
        gameObject.SetActive(false);
    }
}
