using UnityEngine;

abstract class Buff
{
    protected bool remove = false;
    public bool Remove { get { return remove; } }

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
}
