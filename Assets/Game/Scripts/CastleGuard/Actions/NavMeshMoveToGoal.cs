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
		//mNPCInstance.mAnimationListener.mOnAnimatorMove.AddListener(OnAnimatorMove);
	}

	//void OnAnimatorMove()
	//{
	//	if (Time.deltaTime <= 0)
	//	{
	//		return;
	//	}
	//	mNPCInstance.mNavMeshAgent.velocity = mNPCInstance.mAnimator.deltaPosition / Time.deltaTime;
	//}

	public override TaskStatus OnUpdate()
	{
		if(Vector3.Distance(transform.position,mDestination.Value) > mStoppingDistance.Value)
		{
			float aSpeed = Vector3.Project(mNPCInstance.mNavMeshAgent.desiredVelocity, transform.forward).magnitude * mNPCInstance.mNavMeshAgent.speed;
			mNPCInstance.mAnimator.SetFloat(mSpeedParameterName.Value, aSpeed);

			float aAngle = Vector3.Angle(transform.forward, mNPCInstance.mNavMeshAgent.desiredVelocity);
			if (Mathf.Abs(aAngle) <= mDeadZone.Value)
			{
				transform.LookAt(transform.position + mNPCInstance.mNavMeshAgent.desiredVelocity);
			}
			else
			{
				transform.rotation = Quaternion.Lerp(transform.rotation,
													 Quaternion.LookRotation(mNPCInstance.mNavMeshAgent.desiredVelocity),
													 Time.deltaTime * mAngularDampeningTime.Value);
			}
		}
		else
		{
			mNPCInstance.mNavMeshAgent.isStopped = true;
			mNPCInstance.mAnimator.SetFloat(mSpeedParameterName.Value, 0.0f);
		}
		return TaskStatus.Success;
	}

	//public override void OnEnd()
	//{
	//	mNPCInstance.mAnimationListener.mOnAnimatorMove.RemoveListener(OnAnimatorMove);
	//}

}