using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/NavMesh")]
[TaskDescription("Uses Animation to set NPC velocity and stops agent once goal is reached")]
public class NavMeshMoveToGoal : Action
{
	public SharedFloat mAngularDampeningTime = 5.0f;
	public SharedFloat mDeadZone = 10.0f;
	public SharedFloat mStoppingDistance = 0.5f;
	NavMeshNPC mNPCInstance;
	public SharedVector3 mDestination;
	public SharedString mSpeedParameterName = "Speed";
	public override void OnStart()
	{
		if (mNPCInstance == null)
		{
			mNPCInstance = GetComponent<NavMeshNPC>();
		}
	}

	public override TaskStatus OnUpdate()
	{
		if(Vector3.Distance(transform.position,mDestination.Value) > mStoppingDistance.Value)
		{
			Vector3 aVelocityVector = mNPCInstance.mNavMeshAgent.isOnOffMeshLink ?
				(mDestination.Value - transform.position) :
				mNPCInstance.mNavMeshAgent.desiredVelocity;
			float aSpeed = Vector3.Project(aVelocityVector, transform.forward).magnitude * mNPCInstance.mNavMeshAgent.speed;
			mNPCInstance.mAnimator.SetFloat(mSpeedParameterName.Value, aSpeed);
			float aAngle = Vector3.Angle(transform.forward, aVelocityVector);
			if (Mathf.Abs(aAngle) <= mDeadZone.Value)
			{
				transform.LookAt(transform.position + aVelocityVector);
			}
			else
			{
				transform.rotation = Quaternion.Lerp(transform.rotation,
													 Quaternion.LookRotation(aVelocityVector),
													 Time.deltaTime * mAngularDampeningTime.Value);
			}
			if(mNPCInstance.mNavMeshAgent.isOnOffMeshLink)
			{
				transform.position += mNPCInstance.mAnimator.deltaPosition;
				if(Vector3.Distance(transform.position, mNPCInstance.mNavMeshAgent.currentOffMeshLinkData.endPos) <= mStoppingDistance.Value)
				{
					mNPCInstance.mNavMeshAgent.CompleteOffMeshLink();
				}
			}
		}
		else
		{
			if(mNPCInstance.mNavMeshAgent.isOnOffMeshLink)
			{
				mNPCInstance.mNavMeshAgent.CompleteOffMeshLink();
			}
			mNPCInstance.mNavMeshAgent.isStopped = true;
			mNPCInstance.mAnimator.SetFloat(mSpeedParameterName.Value, 0.0f);
		}
		return TaskStatus.Success;
	}

}