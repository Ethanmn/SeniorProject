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
        { typeof(AMischivious), typeof(ANimble), typeof(APatient),
        typeof(ARunner), typeof(AWellTrained), typeof(AWorkHorse) };

    // List of parental attributes to choose from
    private Type[] parentalAttributes =
        { typeof(AAdventurer), typeof(AAntiquarian), typeof(AApothecary),
        typeof(AButcher), typeof(ADeal), typeof(AFarmer),
        typeof(AFletcher), typeof(ARanger)/*, typeof(AWeaver)*/};

    // CONSTANTS END

    // Number of heroes to create
    private int numHero = 3;

    // Number of personal attributes added to a hero
    private int numPerAtt = 2;
    private int numParAtt = 2;

    // List of heroes
    private GameObject[] heroes;
    // List of hero description strings
    private string[] descriptions;

    // Use this for initialization
    void Start () {
        // Init the heroes list
        heroes = new GameObject[numHero];
        // Init the description list
        descriptions = new string[numHero];

        // Cycle through and generate heroes
	    for (int i = 0; i < numHero; i++)
        {
            // Generate the hero's stats and game object
            heroes[i] = GenerateHero();
            HeroStats stats = heroes[i].GetComponent<HeroStats>();

            // Generate the string to describe the hero
            descriptions[i] = GenerateSummary(stats.PersonalAttributes, stats.ParentalAttributes);

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
            //Instantiate(Resources.Load<GameObject>("Prefabs/ChooseHeroButton")).GetComponent<Button>().onClick.AddListener(() => { ChooseHero(i); });
            // Need to add to canvas and set positioning
        }
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

    private string GenerateSummary(List<HeroAttribute> perAttributes, List<HeroAttribute> parAttributes)
    {
        string desc = "";

        // Print the attribute info
        foreach (HeroAttribute atr in perAttributes)
        {
            desc += /*atr.Name + ": " +*/ atr.Description + " " /*+ " (" + atr.Effect + ")\n"*/;
        }
        // Print the attribute info
        foreach (HeroAttribute atr in parAttributes)
        {
            desc += /*atr.Name + ": " +*/ atr.Description + " " //* + " (" + atr.Effect + ")\n"*/;
        }

        print(desc);
        return desc;
    }

    public void ChooseHero(int id)
    {
        // Destroy every hero but the one chosen by 'id'
        // I don't think this is right
        for (int i = 0; i < heroes.Length; i++)
        {
            if (id != i)
            {
                Destroy(heroes[i]);
            }
        }
        // Destroy all buttons
        //???

        // Dissable renderer for hero while the player moves on to choosing other things
        heroes[id].GetComponent<SpriteRenderer>().enabled = false;
    }
}
