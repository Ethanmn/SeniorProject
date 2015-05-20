using UnityEngine;
using System.Collections;
using System;

public class BlobStateDeath : I_NPCState
{
    private float timer;
    private int blinkCount;
    private bool blink;

    void I_NPCState.OnEnter(Transform npc, MobStats stats)
    {
        timer = 0.3f;
        blinkCount = 0;
        blink = false;
        npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void I_NPCState.OnExit(Transform npc)
    {
        
    }

    I_NPCState I_NPCState.Update(Transform npc, float dt)
    {
        npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (blink)
        {
            npc.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            npc.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (timer <= 0)
        {
            GameObject.Destroy(npc.gameObject);
        }

        if (blinkCount == 4)
        {
            blink = !blink;
            blinkCount = 0;
        }
        else
            blinkCount++;

        timer -= dt;
        return null;
    }
    I_NPCState I_NPCState.FixedUpdate(Transform npc, float dt)
    {
        return null;
    }
    I_NPCState I_NPCState.HandleInput(Transform npc)
    {
        return null;
    }

    I_NPCState I_NPCState.OnCollisionEnter(Transform npc, Collision2D c)
    {
        return null;
    }

}
