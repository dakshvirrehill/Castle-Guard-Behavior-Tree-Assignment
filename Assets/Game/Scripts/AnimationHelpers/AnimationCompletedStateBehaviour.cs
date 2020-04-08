using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompletedStateBehaviour : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		IAnimationCompleted aCallback = animator.GetComponent<IAnimationCompleted>();
		if (aCallback != null)
		{
			aCallback.AnimationCompleted(stateInfo.shortNameHash);
		}
	}
}