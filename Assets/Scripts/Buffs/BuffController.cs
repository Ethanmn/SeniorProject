using UnityEngine;
using System.Collections.Generic;

public class BuffController : MonoBehaviour
{
    // List of all buffs on the controller
    //private List<Buff> buffs;
    private Dictionary<string, Buff> buffs;

    public BuffController()
    {
        buffs = new Dictionary<string, Buff>();
    }

    void Awake()
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
                //Debug.Log("Removing " + key);
                buffs.Remove(key);
            }
        }

        // Update each buff
        foreach (Buff buff in buffs.Values)
        {
            buff.OnUpdate();
        }
    }

    void OnDestroy()
    {
        // Update each buff
        foreach (Buff buff in buffs.Values)
        {
            buff.OnEnd();
        }
    }

    /// <summary>
    /// Add a buff to the dictionary. Only one instance of each buff may be added to the collection.
    /// </summary>
    /// <param name="buff">Instance of buff type to be added</param>
    /// <returns>Returns true if the buff is added or if a rune buff level is increased, or false if there is already a copy.</returns>
    public bool AddBuff(Buff buff)
    {
        //Debug.Log(buff.BuffName);
        // IF the buff is already in the dictionary
        if (buffs.ContainsKey(buff.BuffName))
        {
            // Refresh / Add stacks / Whatever
            buffs[buff.BuffName].Refresh();
        }
        // ELSE if it is new
        else
        {
            // On aquiring the buff, do its thang
            buff.OnBegin(transform);

            // Add it to the list of buffs
            buffs.Add(buff.BuffName, buff);

            return true;
        }
        // The buff was not added or was not new (and was not a rune buff)
        return false;
    }

    /// <summary>
    /// Flags a buff to be removed from the dictionary. If it exists and is flagged, returns true, if not returns false.
    /// </summary>
    /// <param name="buff">Instance of buff type to be removed</param>
    /// <returns>Returns true if buff is removed, false if it was not</returns>
    public bool RemoveBuff(Buff buff)
    {
        string key = buff.BuffName;
        // IF the buff exists in the controller
        if (buffs.ContainsKey(key))
        {
            // Flag the buff to be removed in update
            buffs[key].Remove = true;
            return true;
        }
        // ELSE return false
        return false;
    }

    /// <summary>
    /// Flags ALL buffs to removed from the dictionary
    /// </summary>
    public void RemoveAll()
    {
        // Run ending state on ALL buffs
        foreach (string key in buffs.Keys)
        {
            // Flag each buff to be removed
            buffs[key].Remove = true;
        }
    }
}
