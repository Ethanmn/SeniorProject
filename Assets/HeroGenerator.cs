using UnityEngine;
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

    // CONSTANTS END

    // List of heroes
    private GameObject[] heroes;
    // Number of heroes to create
    private int numHero = 3;

    // Number of personal attributes added to a hero
    private int numPerAtt = 2;

	// Use this for initialization
	void Start () {
        heroes = new GameObject[numHero];

	    for (int i = 0; i < numHero; i++)
        {
            heroes[i] = GenerateHero();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private GameObject GenerateHero()
    {
        GameObject hero = Instantiate(Resources.Load<GameObject>("Prefabs/Hero"));
        HeroStats stats = hero.GetComponent<HeroStats>();

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

        List<int> pickedAtt = new List<int>();
        // Choose 2 random personal attributes
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
            stats.Attributes.Add((HeroAttribute)Activator.CreateInstance(personalAttributes[att], new object[] { stats }));
        }

        // Choose 2 random parental attributes

        // Random color alteration
        foreach (HeroAttribute atr in stats.Attributes)
        {
            print(atr.Name + ": " + atr.Description + " (" + atr.Effect + ")");
        }
        return hero;
    }
}
