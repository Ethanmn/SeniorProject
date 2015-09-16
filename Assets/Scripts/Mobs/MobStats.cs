using UnityEngine;
using System.Collections;

public class MobStats : MonoBehaviour {

    public string mobName;
	public int health;
	public float speed;
	public int damage;
	public float aggroRange;
	public bool dead;
    public float deathTimer;

    private int maxHealth;

	void Start()
	{
        maxHealth = health;
    }

	public int Health
	{
		get { return health; }
		set
		{
			health = value;
			if (health <= 0)
			{
				dead = true;
			}
            else
            {
                dead = false;
            }
		}
	}

    public int MaxHealth
    {
        get { return maxHealth; }
    }

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	public int Damage
	{
		get { return damage; }
	}

	public bool Dead
	{
		get { return dead; }
	}
}
