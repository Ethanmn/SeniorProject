using UnityEngine;
using System.Collections;

public class BlobController : MobController {

	public BlobController()
	{
		startState = new BlobStateIdle();
		flinchState = new BlobStateFlinch(Vector2.zero);
	}
}
