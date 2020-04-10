using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("Custom/NavMesh")]
[TaskDescription("Checks if Nav Mesh is on Off Mesh Link")]
public class IsOnOffMeshLink : Conditional
{
	NavMeshNPC mNPCInstance;
	public SharedFloat mOffMeshStoppingDistance = 1.0f;

	public override void OnStart()
	{
		if(mNPCInstance == null)
		{
			mNPCInstance = GetComponent<NavMeshNPC>();
		}
	}

	public override TaskStatus OnUpdate()
	{
		if(mNPCInstance.mNavMeshAgent.isOnOffMeshLink)
		{
			OffMeshLinkData aData = mNPCInstance.mNavMeshAgent.currentOffMeshLinkData;
			return Vector3.Distance(transform.position, aData.endPos) <= mOffMeshStoppingDistance.Value ? TaskStatus.Success : TaskStatus.Failure;
		}
		return TaskStatus.Failure;
	}
}