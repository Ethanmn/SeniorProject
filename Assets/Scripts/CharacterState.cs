using System;
using UnityEngine;

public interface I_CharacterState
{
    void OnEnter(Transform character);
    void OnExit(Transform character);

    // Update is called once per frame
    I_CharacterState Update(Transform character, float dt);
    I_CharacterState HandleInput(Transform character);
    I_CharacterState OnCollisionEnter(Transform character, Collision2D c);
}