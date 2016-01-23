using UnityEngine;
using System.Collections;

public class SparkController : MobController {

	public SparkController()
	{
		startState = new SparkStateAlert();
		flinchState = new SparkStateFlinch(Vector2.zero);
        deathState = new SparkStateDeath();
	}
}
