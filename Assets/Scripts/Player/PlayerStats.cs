using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    // Name strings
    private string firstName = "Alpha";
    private string lastName = "Player";

    // Health
    private int health = 5;

    // Gun stuff
    private int ammo = 6;
    private int maxAmmo = 6;
    private int damage = 1;

    // Movement
    private float speed = 1.0f;
    private float maxSpeed = 4.0f;
    private float slowDown = 0.5f;
    private float dash = 10.0f;
    private float dashTimer = 0.15f;

    // Visibility
    private float alpha = 1f;
    private bool stealth = false;

    // Damage
    private bool flinching = false;
    private float flinchTimer = 1.0f;
    private bool dead = false;
    

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

    public int Health
    {
        get { return health; }

        set
        {
            health = value;

            if (health <= 0)
            {
                Dead = true;
            }
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
            return damage;
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
            return maxSpeed;
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

    public float FlinchTimer
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
}
