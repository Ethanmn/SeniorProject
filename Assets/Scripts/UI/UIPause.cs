using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour {

    KeyCode SHOWMAPKEY = KeyCode.Tab;

    string describeRetire = "Retire to start the dungeon again and pass your weapon down to your heir";
    string confirmRetire = "Click again to confirm";
    string cannotRetire = "Cannot retire while enemies are present";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // IF the end screen or the intro screen aren't up
        if (!(GameObject.Find("Intro") ||
            GameObject.Find("Winner").GetComponent<Image>().color.a > 0 ||
            GameObject.Find("Loser").transform.FindChild("LoserText")))
        {
            // If the pause button is pushed
            if (Input.GetKeyDown(SHOWMAPKEY))
            {
                // Show the description
                foreach (Transform child in transform)
                {
                    // Show the background and text
                    child.gameObject.SetActive(true);
                }

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

                    // Set the number of runes
                    if (hero.GetComponent<HeroInventory>().Runes.Count < 3)
                    {
                        transform.FindChild("RuneCount").GetComponent<Text>().text = "Runes Collected: " + hero.GetComponent<HeroInventory>().Runes.Count;
                    }
                    else
                    {
                        transform.FindChild("RuneCount").GetComponent<Text>().text = "Runes Collected: " + hero.GetComponent<HeroInventory>().Runes.Count + " (MAX)";
                    }
                    
                }
                else
                {
                    eff += "ERROR: Could not find hero.";
                }

                // Set the description
                transform.FindChild("HeroText").GetComponent<Text>().text = eff;
            }

            // Else if it was released
            else if (Input.GetKeyUp(SHOWMAPKEY))
            {
                // Hide the description
                foreach (Transform child in transform)
                {
                    // Hide the background
                    child.gameObject.SetActive(false);
                }

                // Remove the text
                transform.FindChild("HeroText").GetComponent<Text>().text = "";
                transform.FindChild("RetireText").GetComponent<Text>().text = describeRetire;
            }
        }
    }

    public void RetireButton()
    {
        Text retireText = transform.FindChild("RetireText").GetComponent<Text>();
        DungeonManager dm = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();

        // If this is the first time pushing the button
        if (!retireText.text.Equals(confirmRetire))
        {
            if (dm.IsRoomClear())
            {
                // If the room is clear
                retireText.text = "Click again to confirm";
            }
            else
            {
                // If the room is not clear
                retireText.text = cannotRetire;
            }
        }
        else
        {
            // Retire the character
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");

            // If there was a hero (should always be a hero, but for testing purposes there might not be)
            if (hero)
            {
                // Prepare the retired hero
                hero.name = "RetiredHero";
                hero.GetComponent<SpriteRenderer>().enabled = false;
                hero.GetComponent<BuffController>().enabled = false;
                hero.GetComponent<HeroController>().enabled = false;
                hero.GetComponent<HeroAttack>().enabled = false;

                // Remove any excess things attached to the player
                foreach (Transform child in hero.transform)
                {
                    Destroy(child.gameObject);
                }

                // Save the new parent
                DontDestroyOnLoad(hero);
            }
            else
            {
                Debug.LogError("Retire could not find hero!");
            }

            // Load back into the hero picker
            SceneManager.LoadScene("Generation_Demo");
        }
    }
}
