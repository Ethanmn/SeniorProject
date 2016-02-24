using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIHeroDescription : MonoBehaviour {

    KeyCode SHOWMAPKEY = KeyCode.Tab;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // If the pause button is pushed
        if (Input.GetKeyDown(SHOWMAPKEY))
        {
            // Show the description
            foreach (Transform child in transform)
            {
                // Show the background
                child.gameObject.SetActive(true);

                // Set the text
                string eff = "";
                GameObject hero = GameObject.FindGameObjectWithTag("Hero");
                if (hero)
                {
                    HeroStats heroStats = hero.GetComponent<HeroStats>();

                    eff += heroStats.FullName + "\n";

                    // Get the personal attributes
                    List<HeroAttribute> perAttributes = heroStats.PersonalAttributes;
                    List<HeroAttribute> parAttributes = heroStats.ParentalAttributes;

                    // Print the attribute info
                    eff += "\nPersonal Attributes\n";
                    foreach (HeroAttribute atr in perAttributes)
                    {
                        eff += atr.Name + ": " + " (" + atr.Effect + ")\n";
                    }
                    // Print the attribute info
                    eff += "\nParental Attributes\n";
                    foreach (HeroAttribute atr in parAttributes)
                    {
                        eff += atr.Name + ": " + " (" + atr.Effect + ")\n";
                    }
                }
                else
                {
                    eff += "ERROR: Could not find hero.";
                }

                GetComponent<Text>().text = eff;
            }
        }
        // Else if it was released
        else if (Input.GetKeyUp(SHOWMAPKEY))
        {
            // Hide the description
            foreach (Transform child in transform)
            {
                // Hide the background
                child.gameObject.SetActive(false);
                // Remove the text
                GetComponent<Text>().text = "";
            }
        }
    }
}
