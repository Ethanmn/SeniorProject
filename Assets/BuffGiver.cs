using UnityEngine;

public class BuffGiver : MonoBehaviour {

    GameObject hero;
    HeroAttack heroAttack;
    HeroInventory inv;

	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroAttack = hero.GetComponent<HeroAttack>();
        inv = hero.GetComponent<HeroInventory>();

        inv.Equip(new Heirloom(new Sword(hero.transform)));
        inv.Equip(new DeckOfFates());
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Giving buff!");

            //hero.GetComponent<BuffController>().AddBuff(new TestSpeedBuff());

            /* 
                Runes
            */
            inv.Heirloom.AddRune(new DoubleRune());

            //hero.GetComponent<BuffController>().AddBuff(new TumbleBuff());
            //hero.GetComponent<BuffController>().AddBuff(new SteelBuff());
            //hero.GetComponent<BuffController>().AddBuff(new VorpalBuff());
            //hero.GetComponent<BuffController>().AddBuff(new ThirstBuff());
            //hero.GetComponent<BuffController>().AddBuff(new EscapeBuff());
            //hero.GetComponent<BuffController>().AddBuff(new CombustBuff());
            //hero.GetComponent<BuffController>().AddBuff(new ThornBuff());
            //hero.GetComponent<BuffController>().AddBuff(new HungerBuff());
            //hero.GetComponent<BuffController>().AddBuff(new EnrageBuff());
            //hero.GetComponent<BuffController>().AddBuff(new DoubleBuff());
            //hero.GetComponent<BuffController>().AddBuff(new BurnDebuff(5));

            /* 
                Trinkets
            */
            //hero.GetComponent<BuffController>().AddBuff(new DarkMarkBuff());
            //hero.GetComponent<BuffController>().AddBuff(new RunicShieldBuff());
            //hero.GetComponent<BuffController>().AddBuff(new RunicBladeBuff());
            //hero.GetComponent<BuffController>().AddBuff(new BrambleVestBuff());
            //hero.GetComponent<BuffController>().AddBuff(new WingedBootsBuff());
            //hero.GetComponent<BuffController>().AddBuff(new HourglassBuff());

            /* 
                Attributes
            */
            //hero.GetComponent<BuffController>().AddBuff(new WorkhorseBuff());
            //hero.GetComponent<BuffController>().AddBuff(new NimbleBuff());
            //hero.GetComponent<BuffController>().AddBuff(new PatientBuff());
            //hero.GetComponent<BuffController>().AddBuff(new MischiviousBuff());
            //hero.GetComponent<BuffController>().AddBuff(new RunnerBuff());
            //hero.GetComponent<BuffController>().AddBuff(new WellTrainedBuff());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            hero.GetComponent<HeroController>().Heal(1);
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            hero.GetComponent<BuffController>().RemoveAll();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            inv.Heirloom.ChangeWeapon(new Sword(hero.transform));
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            inv.Heirloom.ChangeWeapon(new Lance(hero.transform));
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            inv.Heirloom.ChangeWeapon(new Hammer(hero.transform));
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            inv.Heirloom.ChangeWeapon(new Gun(hero.transform));
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            inv.Heirloom.ChangeWeapon(new Bow(hero.transform));
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            inv.Heirloom.ChangeWeapon(new Orb(hero.transform));
        }

    }
}
