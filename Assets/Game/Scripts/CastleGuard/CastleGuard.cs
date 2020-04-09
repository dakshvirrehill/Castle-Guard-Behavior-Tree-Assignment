using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CastleGuard : MonoBehaviour
{
    public OffMeshLink mBridgeLink;
    public NavMeshAgent mNavMeshAgent;
    public Animator mAnimator;
    [SerializeField] float mHealth = 100;
    public float mDamageAmount = 10;
    public List<Transform> mPatrolWaypoints;
    [HideInInspector]public List<Vector3> mInvestigationPoints;
    public float GuardHealth 
    { 
        get
        {
            return mHealth;
        }
        set
        {
            mHealth = value;
        }
    }

    public int InvestigationPointCount
    {
        get
        {
            return mInvestigationPoints.Count;
        }
        set
        {

        }
    }

}
