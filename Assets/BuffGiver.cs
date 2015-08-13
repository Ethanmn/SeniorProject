using UnityEngine;

public class BuffGiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Giving buff!");

            GameObject pl = GameObject.FindGameObjectWithTag("Hero");

            //pl.GetComponent<BuffController>().AddBuff(new TestSpeedBuff());

            //pl.GetComponent<BuffController>().AddBuff(new NimbleBuff());
            //pl.GetComponent<BuffController>().AddBuff(new SteelBuff());
            //pl.GetComponent<BuffController>().AddBuff(new VorpalBuff());
            //pl.GetComponent<BuffController>().AddBuff(new ThirstBuff());
            //pl.GetComponent<BuffController>().AddBuff(new EscapeBuff());
            //pl.GetComponent<BuffController>().AddBuff(new CombustBuff());
            //pl.GetComponent<BuffController>().AddBuff(new ThornBuff());
            pl.GetComponent<BuffController>().AddBuff(new HungerBuff());
            //pl.GetComponent<BuffController>().AddBuff(new EnrageBuff());
            //pl.GetComponent<BuffController>().AddBuff(new DoubleBuff());
            //pl.GetComponent<BuffController>().AddBuff(new HungerBuff());

            //pl.GetComponent<BuffController>().AddBuff(new DarkMarkBuff());
            //pl.GetComponent<BuffController>().AddBuff(new RunicShieldBuff());
            //pl.GetComponent<BuffController>().AddBuff(new RunicBladeBuff());
            //pl.GetComponent<BuffController>().AddBuff(new BrambleVestBuff());
            //pl.GetComponent<BuffController>().AddBuff(new WingedBootsBuff());
            //pl.GetComponent<BuffController>().AddBuff(new HourglassBuff());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroStats>().Health += 1;
            Debug.Log("Healing for 1!");
        }

    }
}
