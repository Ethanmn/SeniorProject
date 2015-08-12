using UnityEngine;
using System;

public class HeroStats : MonoBehaviour {

    // Name strings
    private string firstName = "Alpha";
    private string lastName = "Hero";

    // Health
    private int maxHealth = 5;
    private int health = 5;

    private int bonusMaxHealth = 0;
    private int bonusDefense = 0;
    private bool undershirt = false;

    // Weapon / Attack
    private int ammo = 6;
    private int maxAmmo = 6;
    private int damage = 1;

    private int enrageDamage = 0;
    private int bonusDamage = 0;
    // Starts at zero, but is only applied if the mult is >= 1
    private int damageMuliplier = 0;

    // Movement
    private float speed = 1.0f;
    private float maxSpeed = 4.0f;
    private float slowDown = 0.5f;
    private float dash = 10.0f;
    private float dashTimer = 0.15f;
    private float tiredTimer = 0.75f;

    private float bonusTiredTimer = 0;
    private float bonusSpeed = 0;

    // Visibility
    private float alpha = 1f;
    private bool stealth = false;

    // Taking Damage
    private bool flinching = false;
    private float flinchTimer = 1.0f;
    private bool dead = false;

    private float flinchTimerBonus = 0;

    // Items / Runes
    private int runeFind = 25;
    private int bonusRuneFind = 0;
    

    public string FirstName
    {
        get { return firstName; }

        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }

        set { lastName = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth + BonusMaxHealth; }

        set
        {
            maxHealth = value;
        }
    }

    public int Health
    {
        get
        {
            // Return only as much health as the character can have
            health = Math.Min(MaxHealth, health);

            return health;
        }

        set
        {
            // Keep a temporary memory of the old health
            int temp = health;

            if (value > MaxHealth)
            {
                Debug.Log("Tried to over-heal to " + value + ", when MAX is " + MaxHealth);
                health = MaxHealth;
            }
            else
                health = value;

            if (health <= 0)
            {
                Dead = true;
                PublisherBox.onDeathPub.RaiseEvent(gameObject.transform);
            }

            // If the hero actually healed, and didn't just "heal" on full health, OR the health decreased the HEALTH CHANGED
            if (!(temp == MaxHealth && value >= temp))
            {
                PublisherBox.onHealthChangePub.RaiseEvent(transform);
            }

            Debug.Log("Health " + health);
        }
    }

    public int BonusDefense
    {
        get
        {
            return bonusDefense;
        }

        set
        {
            bonusDefense = value;
        }
    }

    public int Ammo
    {
        get
        {
            return ammo;
        }

        set
        {
            ammo = value;
            if (ammo >= maxAmmo)
            {
                ammo = maxAmmo;
            }
        }
    }
    public int MaxAmmo
    {
        get
        {
            return maxAmmo;
        }

        set
        {
            maxAmmo = value;
        }
    }

    public int Damage
    {
        get
        {
            // Add the bonus damage to the base damage
            int bDamage = damage + BonusDamage;
            // If the damage mult is less than 1 (either 0 or some fraction) there is either an issue or no multiplier, so just return base damage
            return DamageMuliplier < 1 ? bDamage : bDamage * DamageMuliplier;
        }

        set
        {
            damage = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed + BonusSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public float SlowDown
    {
        get
        {
            return slowDown;
        }

        set
        {
            slowDown = value;
        }
    }

    public float Dash
    {
        get
        {
            return dash;
        }

        set
        {
            dash = value;
        }
    }
    public float DashTimer
    {
        get
        {
            return dashTimer;
        }

        set
        {
            dashTimer = value;
        }
    }
    public float TiredTimer
    {
        get
        {
            return Math.Max(0, tiredTimer - BonusTiredTimer);
        }

        set
        {
            tiredTimer = value;
        }
    }
    public float TiredTimerRaw
    {
        get
        {
            return tiredTimer;
        }

        set
        {
            tiredTimer = value;
        }
    }

    public float Alpha
    {
        get
        {
            return alpha;
        }

        set
        {
            alpha = value;
        }
    }

    public bool Stealth
    {
        get
        {
            return stealth;
        }

        set
        {
            stealth = value;
        }
    }

    public bool Flinching
    {
        get
        {
            return flinching;
        }

        set
        {
            flinching = value;
        }
    }

    public bool Dead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
        }
    }

    public float FlinchTimerRaw
    {
        get
        {
            return flinchTimer;
        }

        set
        {
            flinchTimer = value;
        }
    }

    public float FlinchTimer
    {
        get
        {
            return flinchTimer + FlinchTimerBonus;
        }

        set
        {
            flinchTimer = value;
        }
    }

    public float FlinchTimerBonus
    {
        get
        {
            return flinchTimerBonus;
        }

        set
        {
            flinchTimerBonus = value;
        }
    }

    public int BonusDamage
    {
        get
        {
            return bonusDamage + EnrageDamage;
        }

        set
        {
            bonusDamage = value;
        }
    }

    public int EnrageDamage
    {
        get
        {
            return enrageDamage;
        }

        set
        {
            enrageDamage = value;
        }
    }

    public int BonusMaxHealth
    {
        get
        {
            return bonusMaxHealth;
        }

        set
        {
            bonusMaxHealth = value;
        }
    }

    public int RuneFind
    {
        get
        {
            return runeFind;
        }

        set
        {
            runeFind = value;
        }
    }

    public int BonusRuneFind
    {
        get
        {
            return bonusRuneFind;
        }

        set
        {
            bonusRuneFind = value;
        }
    }

    public bool Undershirt
    {
        get
        {
            return undershirt;
        }

        set
        {
            undershirt = value;
        }
    }

    public int DamageMuliplier
    {
        get
        {
            return damageMuliplier;
        }

        set
        {
            damageMuliplier = value;
        }
    }

    public float BonusTiredTimer
    {
        get
        {
            return bonusTiredTimer;
        }

        set
        {
            bonusTiredTimer = value;
        }
    }

    public float BonusSpeed
    {
        get
        {
            return bonusSpeed;
        }

        set
        {
            bonusSpeed = value;
        }
    }
}
