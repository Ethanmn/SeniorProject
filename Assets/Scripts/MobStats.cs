using UnityEngine;
using System.Collections;

public class MobStats : MonoBehaviour {

    public string mobName;
	public int health;
	public float speed;
	public int damage;
	public float aggroRange;
	public bool dead;

	void Start()
	{

	}

	public int Health
	{
		get { return this.health; }
		set
		{
			health = value;
			if (health <= 0)
			{
				dead = true;
			}
		}
	}

	public float Speed
	{
		get { return this.speed; }
		set { speed = value; }
	}

	public int Damage
	{
		get { return this.damage; }
	}

	public bool Dead
	{
		get { return this.dead; }
	}
}
