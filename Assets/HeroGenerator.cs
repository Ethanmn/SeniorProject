﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class HeroGenerator : MonoBehaviour {

    // CONSTANTS START
    private int MALE = 0;
    private int FEMALE = 1;
    private int NEUTRAL = 2;

    // List of personal attributes to choose from /*typeof(AWellTrained)*/ 
    private Type[] personalAttributes =
        { typeof(AMischievous), typeof(ANimble), typeof(APatient),
        typeof(ARunner), typeof(AStubborn), typeof(AWorkHorse),
        typeof(APerfectionist) };

    // List of parental attributes to choose from
    private Type[] parentalAttributes =
        { typeof(AAdventurer), typeof(AAntiquarian), typeof(AApothecary),
        typeof(AButcher), typeof(ADeal), typeof(AFarmer),
        /*typeof(AFletcher),*/ typeof(ARanger)/*, typeof(AWeaver)*/,
        typeof(ABaker) };

    private Type[] itemAttributes =
        { typeof(ABaker)/*, typeof(AMystic)*/ };

    // List of weapons to choose from
    private Type[] weapons =
        { typeof(Sword), typeof(Lance),  typeof(Hammer),
        typeof(Gun), typeof(Bow), typeof(Orb) };

    // List of runes to choose from
    private Type[] runes =
        { typeof(DoubleRune), typeof(HungerRune),  typeof(SteelRune),
        typeof(ThirstRune), typeof(ThornRune), typeof(VorpalRune) };

    // CONSTANTS END

    // --Configuration vars--

    // Number of heroes to create
    private int numHero = 4;

    // Number of personal attributes added to a hero
    private int numPerAtt = 2;
    private int numParAtt = 2;

    // Number of weapons to choose from
    private int numWeapons = 0;

    // Number of runes to choose from
    private int numRunes = 6;

    // Number of runes a hero is allowed max
    private int maxRunes = 0; // 9 for real max, 5 for demo
    // Number of runes a hero is allowed min
    private int minRunes = 0; 

    // --Configuration end--

    // List of heroes
    private GameObject[] heroes;
    // Chosen hero
    private int chosenHero = 0;
    // List of hero description strings
    private string[] descriptions;

    // List of attributes chosen for heroes
    // Used to stop the same attribute from being chosen more than twice between 3 heroes
    private int[] perAttCount;
    // Used to stop the same attribute from being chosen more than twice between 3 heroes
    private int[] parAttCount;
    // Number of duplicate attributes allowed
    private int maxAttReps = 2;

    // List of runes
    private int[] runesCount;
    private int[] runesCountMin;
    // Number of runes already selected
    private int runesSelected = 0;

    // Parent hero's stats
    private GameObject parent = null;
    // Hero generation
    private int heroGeneration = 0;

    // Use this for initialization
    void Start () {
        // Init the heroes list
        heroes = new GameObject[numHero];
        // Init the description list
        descriptions = new string[numHero];
        // Init the runes list
        runesCount = new int[numRunes];
        runesCountMin = Enumerable.Repeat(0, numRunes).ToArray();
        // Init attribute count list
        perAttCount = new int[personalAttributes.Length];
        // Init attribute count list
        parAttCount = new int[parentalAttributes.Length];

        // Find the parent if it exists
        parent = GameObject.Find("RetiredHero");

        if (parent)
        {
            // Set the generation
            heroGeneration = parent.GetComponent<HeroStats>().Generation + 1;
        }

        // Cycle through and generate heroes that correspond to buttons
        for (int i = 0; i < numHero - 1; i++)
        {
            // Generate the hero's stats and game object
            heroes[i] = GenerateHero();

            HeroStats stats = heroes[i].GetComponent<HeroStats>();

            // Generate the string to describe the hero
            descriptions[i] = GenerateDescription(stats.PersonalAttributes, stats.ParentalAttributes);//GenerateEffects(stats.PersonalAttributes, stats.ParentalAttributes);//

            // For each hero, create a button, allowing the player to choose that hero
            // Choosing is handled by ChooseHero() set off by the button
            GameObject hero = GameObject.Find("HeroDescription" + i);

            // Set the decription
            hero.GetComponent<Text>().text = descriptions[i];

            // Set the name
            hero.transform.Find("ChooseHeroButton").Find("HeroName").GetComponent<Text>().text = stats.FullName;
        }

        // Make a blank
        heroes[numHero - 1] = Instantiate(Resources.Load<GameObject>("Prefabs/Hero"));
        // Disable the controller so the hero won't move around or attack during selection
        heroes[numHero - 1].GetComponent<HeroController>().enabled = false;
        // Disable the attack
        heroes[numHero - 1].GetComponent<HeroAttack>().enabled = false;
        // Dissable renderer for hero while the player moves on to choosing other things
        heroes[numHero - 1].GetComponent<SpriteRenderer>().enabled = false;

        // Set the generation text
        GameObject.Find("GenerationText").GetComponent<Text>().text = "Generation " + heroGeneration;
    }

    private GameObject GenerateHero()
    {
        // Instantiate the hero (because just creating the stats is not possible)
        GameObject hero = Instantiate(Resources.Load<GameObject>("Prefabs/Hero"));
        HeroStats stats = hero.GetComponent<HeroStats>();

        // Disable the controller so the hero won't move around or attack during selection
        hero.GetComponent<HeroController>().enabled = false;
        // Disable the attack
        hero.GetComponent<HeroAttack>().enabled = false;
        // Dissable renderer for hero while the player moves on to choosing other things
        hero.GetComponent<SpriteRenderer>().enabled = false;

        // Set the hero's generation
        stats.Generation = heroGeneration;

        // Choose the given name of the hero
        int gen = UnityEngine.Random.Range(0, 3);

        string gender = "";
        if (gen == MALE)
        {
            gender = "Male";
            stats.Gender = HeroStats.genderE.male;
        }
        else if (gen == FEMALE)
        {
            gender = "Female";
            stats.Gender = HeroStats.genderE.female;
        }
        else if (gen == NEUTRAL)
        {
            gender = "Neutral";
            stats.Gender = HeroStats.genderE.neutral;
        }

        // Choose the first name, based on gender
        TextAsset namesFile = Resources.Load<TextAsset>("Names/" + gender);
        string[] names = namesFile.text.Split('\n');
        int nameNum = UnityEngine.Random.Range(0, names.Length);
        stats.FirstName = names[nameNum];

        // Choose the surname of the hero
        // IF there is a parent, use their surname
        if (parent)
        {
            stats.LastName = parent.GetComponent<HeroStats>().LastName;
        }
        else
        {
            // ELSE do not
            namesFile = Resources.Load<TextAsset>("Names/Surname");
            names = namesFile.text.Split('\n');
            nameNum = UnityEngine.Random.Range(0, names.Length);
            stats.LastName = names[nameNum];
        }

        print("Name: " + stats.FullName);

        // Choose 2 random personal attributes
        List<int> pickedAtt = new List<int>();
        bool haveItem = false;
        for (int i = 0; i < numPerAtt; i++)
        {
            int att = UnityEngine.Random.Range(0, personalAttributes.Length);

            // If this attribute has been picked OR piked the alloted amount of times OR this character already contains an item attribute (can't have two)
            while (pickedAtt.Contains(att) || perAttCount[att] >= maxAttReps || (haveItem /*&& the attribute selected is an item att*/))
            {
                // Get a new one until it hasn't been picked
                att = UnityEngine.Random.Range(0, personalAttributes.Length);
            }

            // Add it to the list of picked numbers
            pickedAtt.Add(att);
            // Increment the number of times that personal attribute has been selected
            perAttCount[att]++;
            //Debug.Log("Personal count " + att + ": " + perAttCount[att]);
            // Add the attribute
            stats.PersonalAttributes.Add((HeroAttribute)Activator.CreateInstance(personalAttributes[att], new object[] { stats }));
        }

        // Choose 2 random parental attributes
        pickedAtt = new List<int>();
        for (int i = 0; i < numParAtt; i++)
        {
            int att = UnityEngine.Random.Range(0, parentalAttributes.Length);

            // If this attribute has been picked
            while (pickedAtt.Contains(att) || parAttCount[att] >= maxAttReps)
            {
                // Get a new one until it hasn't been picked
                att = UnityEngine.Random.Range(0, parentalAttributes.Length);
            }

            // Add it to the list of picked numbers
            pickedAtt.Add(att);
            // Increment the number of times that parental attribute has been selected
            parAttCount[att]++;
            // Add the attribute
            stats.ParentalAttributes.Add((HeroAttribute)Activator.CreateInstance(parentalAttributes[att], new object[] { stats }));
        }

        return hero;
    }

    private string GenerateDescription(List<HeroAttribute> perAttributes, List<HeroAttribute> parAttributes)
    {
        string desc = "";

        // Print the attribute info
        foreach (HeroAttribute atr in perAttributes)
        {
            desc += atr.Description + " ";
        }
        // Print the attribute info
        foreach (HeroAttribute atr in parAttributes)
        {
            desc += atr.Description + " ";
        }

        print(desc);
        return desc;
    }

    private string GenerateEffects(List<HeroAttribute> perAttributes, List<HeroAttribute> parAttributes)
    {
        string eff = "";

        // Print the attribute info
        eff += "Personal Attributes\n";
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

        print(eff);
        return eff;
    }

    // Button functions

    public void ChooseHero(int id)
    {
        // Set the chosen hero
        chosenHero = id;

        // Deactivate every hero but the one chosen by 'id'
        for (int i = 0; i < heroes.Length; i++)
        {
            if (id != i)
            {
                heroes[i].SetActive(false);
            }
        }

        // Dissable the choose hero canvas
        GameObject.Find("ChooseHeroCanvas").SetActive(false);

        // If this is not the first generation, skip straight to the runes
        if (parent)
        {
            HeroInventory parentInv = parent.GetComponent<HeroInventory>();

            // Select the parent's weapon
            Weapon weap = (Weapon)Activator.CreateInstance(parent.GetComponent<HeroInventory>().Heirloom.Weapon.GetType(), new object[] { heroes[chosenHero].transform });
            heroes[chosenHero].GetComponent<HeroInventory>().Equip(new Heirloom(weap));

            // Activate the rune canvas
            foreach (Transform child in GameObject.Find("ChooseRunesCanvas").transform)
            {
                child.gameObject.SetActive(true);
            }

            // Get rune info from parent
            // Count the total number of runes on the Heirloom
            int heirloomRunes = 0;
            foreach (Rune r in parentInv.Heirloom.Runes)
            {
                heirloomRunes += r.Level;
            }
            minRunes = runesSelected = heirloomRunes;
            maxRunes = parentInv.Runes.Count + minRunes;
            
            foreach (Rune r in parentInv.Heirloom.Runes)
            {
                Debug.Log("Rune " + r.GetType() + " " + r.Level);

                if (r.GetType().Equals((typeof(DoubleRune))))
                {
                    runesCountMin[0] = runesCount[0] = r.Level;
                    UpdateRuneText(0);
                }
                else if (r.GetType().Equals((typeof(HungerRune))))
                {
                    runesCountMin[1] = runesCount[1] = r.Level;
                    UpdateRuneText(1);
                }
                else if (r.GetType().Equals((typeof(SteelRune))))
                {
                    runesCountMin[2] = runesCount[2] = r.Level;
                    UpdateRuneText(2);
                }
                else if (r.GetType().Equals((typeof(ThirstRune))))
                {
                    runesCountMin[3] = runesCount[3] = r.Level;
                    UpdateRuneText(3);
                }
                else if (r.GetType().Equals((typeof(ThornRune))))
                {
                    runesCountMin[4] = runesCount[4] = r.Level;
                    UpdateRuneText(4);
                }
                else if (r.GetType().Equals((typeof(VorpalRune))))
                {
                    runesCountMin[5] = runesCount[5] = r.Level;
                    UpdateRuneText(5);
                }
            }

            // Update anyways to make sure the new rune count happens even if no runes are attached
            UpdateRuneText(0);
        }
        else
        {
            // Activate the weapon canvas
            foreach (Transform child in GameObject.Find("ChooseWeaponCanvas").transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void ChooseWeapon(int num)
    {
        // Remove UI pieces
        GameObject.Find("ChooseWeaponCanvas").SetActive(false);
        // Give the hero their weapon
        Weapon weap = (Weapon)Activator.CreateInstance(weapons[num], new object[] { heroes[chosenHero].transform });
        heroes[chosenHero].GetComponent<HeroInventory>().Equip(new Heirloom(weap));

        // Activate the rune canvas
        foreach (Transform child in GameObject.Find("ChooseRunesCanvas").transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void ChooseRune(int num)
    {
        // IF runes[num] is < 3 AND total of all values of runes < 9
        if (runesCount[num] < 3 && runesSelected < maxRunes)
        {
            // Add one to that rune selection
            runesCount[num] += 1;
            // Increment the total rune count
            runesSelected += 1;

            UpdateRuneText(num);
        }
    }

    public void UnchooseRune(int num)
    {
        if (runesCount[num] > runesCountMin[num] &&
            runesSelected - 1 >= minRunes)
        {
            // Remove one from rune selection
            runesCount[num] -= 1;
            // Decrement total rune count
            runesSelected -= 1;

            UpdateRuneText(num);
        }
    }

    /// <summary>
    /// Helper function for updating rune UI text
    /// </summary>
    /// <param name="num">Rune number passed by the button</param>
    private void UpdateRuneText(int num)
    {
        // Update the count text
        GameObject.Find("NumRunes" + num).GetComponent<Text>().text = runesCount[num] + "/3";

        // Set the text
        foreach (Transform child in GameObject.Find("ChooseRune" + num).transform)
        {
            // Enable the text corresponding to the rune currently picked
            child.gameObject.SetActive(true);
            if (child.name == "RuneDescription" + num + runesCount[num])
            {
                child.gameObject.SetActive(true);
            }
            // Dissable text that isn't the level currently picked
            else if (child.name != "RuneButton" + num + "+" && 
                child.name != "RuneButton" + num + "-" &&
                child.name != "NumRunes" + num &&
                child.name != "RuneImage" + num)
            {
                child.gameObject.SetActive(false);
            }
        }

        // Update total runes text
        GameObject.Find("TotalRunes").GetComponent<Text>().text = "Runes " + runesSelected + "/" + maxRunes;
        if (maxRunes > 0)
            GameObject.Find("TotalRunes").GetComponent<Text>().color = Color.white;
    }

    public void ConfirmRunes()
    {
        for (int i = 0; i < runesCount.Length; i++)
        {
            for (int j = 0; j < runesCount[i]; j++)
            {
                heroes[chosenHero].GetComponent<HeroInventory>().Heirloom.AddRune((Rune)Activator.CreateInstance(runes[i]));
            }
        }

        // Deactivate the rune canvas
        GameObject.Find("ChooseRunesCanvas").SetActive(false);

        // After runes are confirmed, start the game
        StartGame();
    }

    private void StartGame()
    {
        // Destroy the parent
        if (parent)
        {
            Destroy(parent);
        }

        // Start the dungeon
        DontDestroyOnLoad(heroes[chosenHero]);
        DontDestroyOnLoad(GameObject.Find("ExitApp"));
        SceneManager.LoadScene("Dungeon");

        // Activate the hero
        heroes[chosenHero].SetActive(true);
        heroes[chosenHero].transform.position = new Vector3(1000, 1000, 1000);
        heroes[chosenHero].GetComponent<SpriteRenderer>().enabled = true;
        heroes[chosenHero].GetComponent<HeroController>().enabled = true;
        heroes[chosenHero].GetComponent<HeroAttack>().enabled = true;

        // IF the hero has any item attributes
        foreach (HeroAttribute att in heroes[chosenHero].GetComponent<HeroStats>().ParentalAttributes)
        {
            if (att is ABaker)
            {
                // IF they have Baker, give them bread
                heroes[chosenHero].GetComponent<HeroInventory>().Equip(new LoafOfBread());
            }
        }
    }
}
