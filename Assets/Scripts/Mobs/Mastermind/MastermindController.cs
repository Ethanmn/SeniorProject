using UnityEngine;
using System.Collections;

public class MastermindController : MobController {

	public MastermindController()
	{
		startState = new MastermindStateIdle();
		flinchState = new MastermindStateFlinch(Vector2.zero);
        deathState = new MastermindStateDeath();
	}
}
