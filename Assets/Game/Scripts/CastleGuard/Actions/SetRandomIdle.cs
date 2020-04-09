using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("CastleGuard/Animation")]
[TaskDescription("Sets a random Idle index and triggers idle animation")]
public class SetRandomIdle : Action
{
	NavMeshNPC mNPCInstance;
	public SharedInt mIdleCount = 4;
	public SharedString mIdleTriggerName = "Idle";
	public SharedString mIdleIndexName = "IdleIndex";
	public override void OnStart()
	{
		if(mNPCInstance == null)
		{
			mNPCInstance = GetComponent<NavMeshNPC>();
		}
	}

	public override TaskStatus OnUpdate()
	{
		mNPCInstance.mAnimator.SetInteger(mIdleIndexName.Value, Random.Range(0, mIdleCount.Value));
		mNPCInstance.mAnimator.SetTrigger(mIdleTriggerName.Value);
		return TaskStatus.Success;
	}
}