using UnityEngine;
using System.Collections.Generic;

class BuffController : MonoBehaviour
{
    // List of all buffs on the controller
    private List<Buff> buffs;

    public BuffController()
    {
        buffs = new List<Buff>();
    }

    void Start()
    {

    }

    void Update()
    {
        // Check for buffs that are flagged to be removed
        foreach (Buff buff in buffs)
        {
            if (buff.Remove)
            {
                buff.OnEnd();
            }
        }

        // Remove all buffs that are flagged to be removed
        buffs.RemoveAll(IsExhausted);

        // Update each buff
        foreach (Buff buff in buffs)
        {
            buff.OnUpdate();
        }
    }

    public void AddBuff(Buff buff)
    {
        // On aquiring the buff, do its thang
        buff.OnBegin(gameObject.GetComponent<Transform>());

        // Add it to the list of buffs
        buffs.Add(buff);
    }

    // Search predicate returns true if the duration is exhausted (<= 0)
    private static bool IsExhausted(Buff buff)
    {
        return buff.Remove;
    }
}
