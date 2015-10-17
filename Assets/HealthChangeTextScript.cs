﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthChangeTextScript : MonoBehaviour {

    Animator textAnim;
    Text text;

    // Use this for initialization
    void Start () {
        text = gameObject.GetComponent<Text>();
        if (Convert.ToInt32(text.text) < 0)
        {
            text.color = new Color(1, 0, 0, 1);
        }
        else if (Convert.ToInt32(text.text) > 0)
        {
            text.color = new Color(0, 1, 0, 1);
        }
        
        textAnim = gameObject.GetComponent<Animator>();
        // Wait for animation to finish
        StartCoroutine(AnimationYield(1));
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private IEnumerator AnimationYield(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
