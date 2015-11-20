using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Hero"));
        Application.LoadLevel("Generation_Demo");
    }
}
