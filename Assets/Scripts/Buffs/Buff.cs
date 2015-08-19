using UnityEngine;

abstract class Buff
{
    // Flag the buff as needing to be removed
    protected bool remove = false;
    // Accessor to remove flag
    public bool Remove { get { return remove; } }

    // Buff's name / key
    protected string buffName;
    public string BuffName
    {
        get
        {
            return buffName;
        }
    }

    // The private field that holds the character's transform
    protected Transform chr;
    // Property for the buff's character (who the buff controller is on)
    public Transform Character { get { return chr; } }

    // Method to be run when a buff is first applied
    abstract public void OnBegin(Transform character);

    // Method to be run when a buff expires or is removed
    abstract public void OnEnd();

    // Method to be run on a buff every update frame
    abstract public void OnUpdate();

    // Constructor
    public Buff()
    {
        buffName = GetType().Name;
    }
}
