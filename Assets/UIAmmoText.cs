using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAmmoText : MonoBehaviour {

    private GameObject hero;

	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Hero");
	}
	
	// Update is called once per frame
	void Update () {
        if (hero.GetComponent<HeroInventory>().Heirloom.Weapon.GetType().IsSubclassOf(typeof(RangedWeapon)))
        {
            GetComponent<Text>().text = hero.GetComponent<HeroStats>().Ammo.ToString();
        }
	}
}
