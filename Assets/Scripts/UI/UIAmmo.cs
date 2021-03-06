﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour
{

    private GameObject hero;
    private HeroInventory inv;
    private HeroStats heroStats;

    // List of all bullets in the UI
    private List<GameObject> bullets;
    private int numBullets = 6;

    // List of all orbs in the UI
    private List<GameObject> orbs;
    private int numOrbs = 4;

    // The hero's weapon
    Weapon weapon;

    // Use this for initialization
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        if (!hero)
        {
            // If there is no hero, just escape instead
            return;
        }
        inv = hero.GetComponent<HeroInventory>();
        heroStats = hero.GetComponent<HeroStats>();

        weapon = inv.Heirloom.Weapon;

        bullets = new List<GameObject>();
        orbs = new List<GameObject>();

        // Subscribe to the OnHealthChangeEvent to handle ammo
        PublisherBox.onAmmoChangePub.RaiseOnAmmoChangeEvent += HandleOnAmmoChangeEvent;

        // Create the bullet stuff image, then deactivate them
        GameObject bulletIm = Resources.Load<GameObject>("Prefabs/UIAmmoBullet");

        for (int i = 0; i < numBullets; i++)
        {
            GameObject bullet = Instantiate(bulletIm);
            Vector2 pos = Vector2.zero;
            // Determin the position
            switch(i)
            {
                case 0:
                    pos = new Vector2(0, 45);
                    break;
                case 1:
                    pos = new Vector2(40, 22);
                    break;
                case 2:
                    pos = new Vector2(40, -22);
                    break;
                case 3:
                    pos = new Vector2(0, -45);
                    break;
                case 4:
                    pos = new Vector2(-40, -22);
                    break;
                case 5:
                    pos = new Vector2(-40, 22);
                    break;
                default:
                    break;
            }

            // Set the parent to this object
            bullet.transform.SetParent(gameObject.transform);
            bullet.transform.localScale = new Vector3(1, 1, 1);
            bullet.transform.localPosition = pos;
            bullet.SetActive(false);
            bullets.Add(bullet);
        }

        // Create the bullet stuff image, then deactivate them
        GameObject orbIm = Resources.Load<GameObject>("Prefabs/UIAmmoOrb");
        for (int i = 0; i < numOrbs; i++)
        {
            GameObject orb = Instantiate(orbIm);
            Vector2 pos = Vector2.zero;
            // Determin the position
            switch (i)
            {
                case 3:
                    pos = new Vector2(-30, 32);
                    break;
                case 2:
                    pos = new Vector2(30, 23);
                    break;
                case 1:
                    pos = new Vector2(-30, -25);
                    break;
                case 0:
                    pos = new Vector2(30, -38);
                    break;
            }

            // Set the parent to this object
            orb.transform.SetParent(gameObject.transform);
            orb.transform.localScale = new Vector3(1, 1, 1);
            orb.transform.localPosition = pos;
            orb.SetActive(false);
            orbs.Add(orb);
        }

        // Preliminary draw
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        // Unsubscribe to the OnHealthChangeEvent so that it doesn't run on a UI that doesn't exist
        PublisherBox.onAmmoChangePub.RaiseOnAmmoChangeEvent -= HandleOnAmmoChangeEvent;
    }

    private void HandleOnAmmoChangeEvent(object sender, POnAmmoChangeEventArgs e)
    {
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        Image ammoHolder = GetComponent<Image>();
        if (ammoHolder == null)
        {
            return;
        }

        if (weapon.GetType().IsSubclassOf(typeof(RangedWeapon)))
        {
            // Make it visible
            ammoHolder.color = new Color(1, 1, 1, 1);

            // If it is a gun
            if (hero.GetComponent<HeroInventory>().Heirloom.Weapon.GetType() == typeof(Gun))
            {
                ammoHolder.sprite = Resources.LoadAll<Sprite>("sprites/AmmoHolderPH")[0];
                // Check number of bullets
                // Anything after the ammo number is empty
                for (int i = heroStats.Ammo; i < heroStats.MaxAmmo; i++)
                {
                    bullets[i].SetActive(false);
                }

                // Anything before the ammo is full
                for (int i = heroStats.Ammo - 1; i >= 0; i--)
                {
                    bullets[i].SetActive(true);
                }
            }
            else
            {
                foreach (GameObject bullet in bullets)
                {
                    bullet.SetActive(false);
                }
            }

            // If it is an orb
            if (weapon.GetType() == typeof(Orb))
            {
                ammoHolder.sprite = Resources.LoadAll<Sprite>("sprites/AmmoHolderPH")[1];
                // Check number of magiks
                // Check number of bullets
                for (int i = heroStats.Ammo; i < heroStats.MaxAmmo; i++)
                {
                    // Anything after the ammo number is empty
                    orbs[i].SetActive(false);
                }

                // Anything before the ammo is full
                for (int i = heroStats.Ammo - 1; i >= 0; i--)
                {
                    orbs[i].SetActive(true);
                }
            }
            else
            {
                foreach (GameObject orb in orbs)
                {
                    orb.SetActive(false);
                }
            }

            // If it is a bow
            if (weapon.GetType() == typeof(Bow))
            {
                int tier = ((Bow)weapon).Tier;
                // Set it to the quiver
                ammoHolder.sprite = Resources.LoadAll<Sprite>("sprites/UI/UIBow")[tier];

                // Deactivate all ammo
                foreach (GameObject bullet in bullets)
                {
                    bullet.SetActive(false);
                }
                foreach (GameObject orb in orbs)
                {
                    orb.SetActive(false);
                }
            }

        }
        else
        {
            // If it is a melee weapon
            // Make it invisble
            ammoHolder.color = new Color(1, 1, 1, 0);

            // Deactivate all ammo
            foreach (GameObject bullet in bullets)
            {
                bullet.SetActive(false);
            }
            foreach (GameObject orb in orbs)
            {
                orb.SetActive(false);
            }
        }
    }
}
