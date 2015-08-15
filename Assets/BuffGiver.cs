using UnityEngine;

public class BuffGiver : MonoBehaviour {

    GameObject hero;
    HeroAttack heroAttack;

	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroAttack = hero.GetComponent<HeroAttack>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Giving buff!");

            //hero.GetComponent<BuffController>().AddBuff(new TestSpeedBuff());

            //hero.GetComponent<BuffController>().AddBuff(new NimbleBuff());
            //hero.GetComponent<BuffController>().AddBuff(new SteelBuff());
            //hero.GetComponent<BuffController>().AddBuff(new VorpalBuff());
            //hero.GetComponent<BuffController>().AddBuff(new ThirstBuff());
            //hero.GetComponent<BuffController>().AddBuff(new EscapeBuff());
            //hero.GetComponent<BuffController>().AddBuff(new CombustBuff());
            //hero.GetComponent<BuffController>().AddBuff(new ThornBuff());
            //hero.GetComponent<BuffController>().AddBuff(new HungerBuff());
            //hero.GetComponent<BuffController>().AddBuff(new EnrageBuff());
            //hero.GetComponent<BuffController>().AddBuff(new DoubleBuff());
            //hero.GetComponent<BuffController>().AddBuff(new HungerBuff());

            //hero.GetComponent<BuffController>().AddBuff(new DarkMarkBuff());
            //hero.GetComponent<BuffController>().AddBuff(new RunicShieldBuff());
            //hero.GetComponent<BuffController>().AddBuff(new RunicBladeBuff());
            //hero.GetComponent<BuffController>().AddBuff(new BrambleVestBuff());
            //hero.GetComponent<BuffController>().AddBuff(new WingedBootsBuff());
            //hero.GetComponent<BuffController>().AddBuff(new HourglassBuff());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            hero.GetComponent<HeroStats>().Health += 1;
            Debug.Log("Healing for 1!");
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Sword(hero.transform);
            heroAttack.weapon.OnEquip();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Lance(hero.transform);
            heroAttack.weapon.OnEquip();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Hammer(hero.transform);
            heroAttack.weapon.OnEquip();
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Gun(hero.transform);
            heroAttack.weapon.OnEquip();
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Bow(hero.transform);
            heroAttack.weapon.OnEquip();
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            heroAttack.weapon.OnUnequip();
            heroAttack.weapon = new Orb(hero.transform);
            heroAttack.weapon.OnEquip();
        }

    }
}
