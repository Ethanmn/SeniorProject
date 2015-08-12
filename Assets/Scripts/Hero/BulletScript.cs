using UnityEngine;

public class BulletScript : MonoBehaviour {
	
	private Vector2 vel;
	private int damage = 1;

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


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		vel = gameObject.GetComponent<Rigidbody2D>().velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Mob"))
		{
            if (!c.GetComponent<MobStats>().Dead)
            {
                // Raise the event that an enemy was hit, and send which enemy was hit
                PublisherBox.onHitPub.RaiseEvent(c.GetComponent<Transform>(), damage);
                c.GetComponent<MobController>().Hit(this.damage, this.vel / 7f);
            }
			GameObject.Destroy(gameObject);
		}
        else if (c.CompareTag("Wall"))
        {
            GameObject.Destroy(gameObject);
        }
	}

	public void SetVel(Vector2 vel)
	{
		this.vel = vel;
	}

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}
}
