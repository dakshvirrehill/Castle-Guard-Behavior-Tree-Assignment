using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNPC : MonoBehaviour
{
    public NavMeshAgent mNavMeshAgent;
    public Animator mAnimator;
    public AnimationListener mAnimationListener;

    protected virtual void Awake()
    {
        mNavMeshAgent.isStopped = true;
    }

    void OnAnimatorMove()
    {
        if(Time.deltaTime > 0)
        {
            if(mNavMeshAgent.desiredVelocity.sqrMagnitude != 0)
            {
                mNavMeshAgent.velocity = mAnimator.deltaPosition / Time.deltaTime;
            }
        }
    }
}
