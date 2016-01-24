using System;
using UnityEngine;

public abstract class Rune : Item
{
    // Hero's buff controller (to add buffs)
    private BuffController buffCon;

    // The buff the trinket confers
    protected RuneBuff buff;
    public RuneBuff Buff
    {
        get { return buff; }
    }

    // Level of the rune
    protected int level;

    // Sprites for each level
    private Sprite[] spriteSet;

    public Rune() : base()
    {
        spriteSet = new Sprite[3];
        buff = null;
        level = 1;
    }

    /// <summary>
    /// When a rune is equiped, add its buff to the buff controller
    /// </summary>
    public override void OnEquip(Transform chr)
    {
        // Get the buff controller
        buffCon = chr.GetComponent<BuffController>();

        // Add buff to buff controller
        buffCon.AddBuff(buff);
    }

    /// <summary>
    /// When a rune is removed, remove its buff from the buff controller
    /// </summary>
    public override void OnUnequip()
    {
        // Remove buff from buff controller
        buffCon.RemoveBuff(buff);
        // Remove a level
        level--;
    }

    /// <summary>
    /// Increase the level of the rune
    /// </summary>
    public void LevelUp()
    {
        if (level < 3)
        {
            level++;
        }
        else
        {
            Debug.Log("Rune is maxed!");
        }
        // Add the buff
        buffCon.AddBuff(buff);
    }

    public override void Drop(Vector3 pos)
    {
        // Instantiate the item prefab
        GameObject it = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Item")) as GameObject;
        // Attach it to the room, so it doesn't teleport between rooms
        it.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform, false);
        // Set it to the item to be dropped
        it.GetComponent<ItemObjectScript>().Item = this;
        // Drop it
        // Probably need more logic to not drop things outside of the play area
        it.transform.position = pos;
    }

    public override void OnDestroy()
    {
        OnUnequip();
    }
}
