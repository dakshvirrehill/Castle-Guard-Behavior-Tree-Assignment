using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/NavMesh")]
[TaskDescription("Sets the destination the agent should go to if waypoints are available")]
public class NavMeshWaypointPatrol : Action
{
	int mWaypointIndex = -1;
	public SharedTransformList mWaypoints;
	public SharedVector3 mDestination;

	public override void OnStart()
	{
		if(mWaypoints.Value.Count != 0)
		{
			mWaypointIndex = (mWaypointIndex + 1) % mWaypoints.Value.Count;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if (mWaypointIndex < 0 || mWaypointIndex > mWaypoints.Value.Count)
			return TaskStatus.Failure;
		mDestination.Value = mWaypoints.Value[mWaypointIndex].position;
		return TaskStatus.Success;
	}
	public override float GetPriority()
	{
		return 5;
	}
}