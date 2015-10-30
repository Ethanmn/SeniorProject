using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour {

    // Direction the player exits (defined by Tiled attribute)
    protected string exitDir;
    public string ExitDir
    {
        get { return exitDir; }
        set
        {
            exitDir = value.ToString().ToLower();
        }
    }

	// Use this for initialization
	public virtual void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public virtual void SetDirection()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Hero"))
        {
            GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>().MoveRoom(exitDir);
        }
    }
}
