using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("CastleGuard/Navigation")]
[TaskDescription("Finds the closest investigation destination and sets that as the destination")]
public class InvestigatePoint : Action
{
	CastleGuard mGuard;
	public SharedVector3 mDestination;
	public override void OnStart()
	{
		if(mGuard == null)
		{
			mGuard = GetComponent<CastleGuard>();
		}
	}

	public override TaskStatus OnUpdate()
	{
		float aMinDistance = float.MaxValue;
		int aSelectedIx = -1;
		for(int aI = 0; aI < mGuard.mInvestigationPoints.Count; aI ++)
		{
			float aDistance = Vector3.Distance(transform.position, mGuard.mInvestigationPoints[aI]);
			if(aDistance < aMinDistance)
			{
				aMinDistance = aDistance;
				aSelectedIx = aI;
			}
		}
		mDestination.Value = mGuard.mInvestigationPoints[aSelectedIx];
		mGuard.mInvestigationPoints.RemoveAt(aSelectedIx);
		return TaskStatus.Success;
	}

	public override float GetPriority()
	{
		return 20;
	}

}