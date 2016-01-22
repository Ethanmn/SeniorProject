using UnityEngine;
using System.Collections;

public class SparkController : MobController {

	public SparkController()
	{
		startState = new SparkStateIdle();
		flinchState = new SparkStateFlinch(Vector2.zero);
        deathState = new SparkStateDeath();
	}
}
