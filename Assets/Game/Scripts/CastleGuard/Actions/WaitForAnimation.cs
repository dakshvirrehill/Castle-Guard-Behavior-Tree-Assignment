using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Animation")]
[TaskDescription("Waits for Animation")]
public class WaitForAnimation : Action
{
	public SharedString mAnimName;
	int mAnimHash;
	NavMeshNPC mNPCInstance;

	bool mAnimationCompleted = false;

	public override void OnStart()
	{
		if(mNPCInstance == null)
		{
			mAnimHash = Animator.StringToHash(mAnimName.Value);
			mNPCInstance = GetComponent<NavMeshNPC>();
		}
		mNPCInstance.mAnimationListener.RegisterOnAnimationCompleted(mAnimHash, OnAnimationCompleted);
		mAnimationCompleted = false;
	}

	void OnAnimationCompleted(int pAnimHash)
	{
		mAnimationCompleted = pAnimHash == mAnimHash;
	}

	public override TaskStatus OnUpdate()
	{
		return mAnimationCompleted ? TaskStatus.Success : TaskStatus.Running;
	}

	public override void OnEnd()
	{
		mNPCInstance.mAnimationListener.UnregisterOnAnimationCompleted(mAnimHash, OnAnimationCompleted);
	}
}