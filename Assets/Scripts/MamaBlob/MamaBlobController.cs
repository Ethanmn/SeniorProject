using UnityEngine;
using System.Collections;

public class MamaBlobController : MobController {

	public MamaBlobController()
	{
		startState = new MamaBlobStateIdle();
		flinchState = new MamaBlobStateFlinch(Vector2.zero);
        deathState = new MamaBlobStateDeath();
	}
}
