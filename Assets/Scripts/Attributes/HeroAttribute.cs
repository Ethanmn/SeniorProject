using UnityEngine;

public class HeroAttribute
{
    // Name of the attribute
    protected string name;
    // Description of the attribute
    protected string description;
    // Actual string of effect
    protected string effect;
    // Buff associate with the attribute
    protected AttributeBuff buff;

   
    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public string Effect
    {
        get { return effect; }
    }

    public HeroAttribute(HeroStats stats)
    {

    }

    public virtual void OnAdd(BuffController cont)
    {
        cont.AddBuff(buff);
    }
}
