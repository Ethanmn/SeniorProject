using UnityEngine;
using System.Collections.Generic;

class BuffController : MonoBehaviour
{
    // List of all buffs on the controller
    //private List<Buff> buffs;
    private Dictionary<string, Buff> buffs;

    public BuffController()
    {
        buffs = new Dictionary<string, Buff>();
    }

    void Update()
    {
        // Check for buffs that are flagged to be removed
        List<string> keys = new List<string>(buffs.Keys);
        foreach (string key in keys)
        {
            if (buffs[key].Remove)
            {
                buffs[key].OnEnd();
                Debug.Log("Removing " + key);
                buffs.Remove(key);
            }
        }

        // Remove all buffs that are flagged to be removed
        //buffs.RemoveAll(IsExhausted);

        // Update each buff
        foreach (Buff buff in buffs.Values)
        {
            buff.OnUpdate();
        }
    }

    public void AddBuff(Buff buff)
    {
        // IF the buff is already in the dictionary
        if (buffs.ContainsKey(buff.BuffName))
        {
            buffs[buff.BuffName].AddStack();
        }
        // ELSE if it is new
        else
        {
            // On aquiring the buff, do its thang
            buff.OnBegin(transform);

            // Add it to the list of buffs
            buffs.Add(buff.BuffName, buff);
        }
    }

    public void RemoveBuff(Buff buff)
    {
        string key = buff.BuffName;
        // IF the buff exists in the controller
        if (buffs.ContainsKey(key))
        {
            // Run the ending method
            buffs[key].OnEnd();
            // Remove the buff
            buffs.Remove(key);
        }
    }
}
