using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class HeroGenerator : MonoBehaviour {

    // CONSTANTS START
    private int MALE = 0;
    private int FEMALE = 1;
    private int NEUTRAL = 2;

    // List of personal attributes to choose from
    private Type[] personalAttributes =
        { typeof(AMischievous), typeof(ANimble), typeof(APatient),
        typeof(ARunner), typeof(AWellTrained), typeof(AWorkHorse) };

    // List of parental attributes to choose from
    private Type[] parentalAttributes =
        { typeof(AAdventurer), typeof(AAntiquarian), typeof(AApothecary),
        typeof(AButcher), typeof(ADeal), typeof(AFarmer),
        /*typeof(AFletcher),*/ typeof(ARanger)/*, typeof(AWeaver)*/};

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
    private int numHero = 3;

    // Number of personal attributes added to a hero
    private int numPerAtt = 2;
    private int numParAtt = 2;

    // Number of weapons to choose from
    private int numWeapons = 0;

    // Number of runes to choose from
    private int numRunes = 6;

    // Number of runes a hero is allowed max
    private int maxRunes = 5; // 9 for real max, 5 for demo

    // --Configuration end--

    // List of heroes
    private GameObject[] heroes;
    // Chosen hero
    private int chosenHero = 0;
    // List of hero description strings
    private string[] descriptions;

    // List of runes
    private int[] runesCount;
    // Number of runes already selected
    private int runesSelected = 0;

    // Use this for initialization
    void Start () {
        // Init the heroes list
        heroes = new GameObject[numHero];
        // Init the description list
        descriptions = new string[numHero];
        // Init the runes list
        runesCount = new int[numRunes];

        // Cycle through and generate heroes that correspond to buttons
	    for (int i = 0; i < numHero; i++)
        {
            // Generate the hero's stats and game object
            heroes[i] = GenerateHero();
            HeroStats stats = heroes[i].GetComponent<HeroStats>();

            // Generate the string to describe the hero
            descriptions[i] = GenerateEffects(stats.PersonalAttributes, stats.ParentalAttributes);//GenerateDescription(stats.PersonalAttributes, stats.ParentalAttributes);

            // For each hero, create a button, allowing the player to choose that hero
            // Choosing is handled by ChooseHero() set off by the button
            GameObject button = GameObject.Find("ChooseHeroButton" + i);
            Text[] texts = button.GetComponentsInChildren<Text>();
            foreach (Text t in texts)
            {
                if (t.name == "HeroName")
                {
                    t.text = stats.FullName;
                }
                if (t.name == "HeroDescription")
                {
                    t.text = descriptions[i];
                }
            }
        }

        // Choose some runes
        

        // Show a summary
        // GenerateHeroSummary()
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
        namesFile = Resources.Load<TextAsset>("Names/Surname");
        names = namesFile.text.Split('\n');
        nameNum = UnityEngine.Random.Range(0, names.Length);
        stats.LastName = names[nameNum];

        print("Name: " + stats.FullName);

        // Choose 2 random personal attributes
        List<int> pickedAtt = new List<int>();
        for (int i = 0; i < numPerAtt; i++)
        {
            int att = UnityEngine.Random.Range(0, personalAttributes.Length);
            
            // If this attribute has been picked
            while (pickedAtt.Contains(att))
            {
                // Get a new one until it hasn't been picked
                att = UnityEngine.Random.Range(0, personalAttributes.Length);
            }

            // Add it to the list of picked numbers
            pickedAtt.Add(att);
            // Add the attribute
            stats.PersonalAttributes.Add((HeroAttribute)Activator.CreateInstance(personalAttributes[att], new object[] { stats }));
        }

        // Choose 2 random parental attributes
        pickedAtt = new List<int>();
        for (int i = 0; i < numParAtt; i++)
        {
            int att = UnityEngine.Random.Range(0, parentalAttributes.Length);

            // If this attribute has been picked
            while (pickedAtt.Contains(att))
            {
                // Get a new one until it hasn't been picked
                att = UnityEngine.Random.Range(0, parentalAttributes.Length);
            }

            // Add it to the list of picked numbers
            pickedAtt.Add(att);
            // Add the attribute
            stats.ParentalAttributes.Add((HeroAttribute)Activator.CreateInstance(parentalAttributes[att], new object[] { stats }));
        }

        // Random color alteration (This probably won't matter later, just for demo purposes)
        float r, g, b;
        r = UnityEngine.Random.Range(0.1f, 0.9f);
        g = UnityEngine.Random.Range(0.1f, 0.9f);
        b = UnityEngine.Random.Range(0.1f, 0.9f);
        stats.ColorAlteration = new Color(r, g, b);
        hero.GetComponent<SpriteRenderer>().color = stats.ColorAlteration;

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
        foreach (HeroAttribute atr in perAttributes)
        {
            eff += atr.Name + ": " + " (" + atr.Effect + ")\n";
        }
        // Print the attribute info
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

        // Activate the weapon canvas
        foreach (Transform child in GameObject.Find("ChooseWeaponCanvas").transform)
        {
            child.gameObject.SetActive(true);
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
        // IF runes[num] is > 0
        if (runesCount[num] > 0)
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
                child.name != "NumRunes" + num)
            {
                child.gameObject.SetActive(false);
            }
        }

        // Update total runes text
        GameObject.Find("TotalRunes").GetComponent<Text>().text = "Runes " + runesSelected + "/" + maxRunes;
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

        // Activate the hero
        heroes[chosenHero].SetActive(true);
        heroes[chosenHero].GetComponent<SpriteRenderer>().enabled = true;
        heroes[chosenHero].GetComponent<HeroController>().enabled = true;
        heroes[chosenHero].GetComponent<HeroAttack>().enabled = true;
        heroes[chosenHero].GetComponent<HeroInventory>().Equip(new StoneSkinSalve());

        // Start the dungeon
        DontDestroyOnLoad(heroes[chosenHero]);
        DontDestroyOnLoad(GameObject.Find("ExitApp"));
        Application.LoadLevel("Dungeon");
    }
}
