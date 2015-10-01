using UnityEngine;
using System.Collections;

public interface I_MobState : I_ActorState {
	// Update is called once per frame
    I_MobState FixedUpdate(Transform mob, float dt);
}
