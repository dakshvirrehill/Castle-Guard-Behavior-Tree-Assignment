using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CastleGuard : NavMeshNPC
{
    public OffMeshLink mBridgeLink;
    [SerializeField] float mHealth = 100;
    public float mDamageAmount = 10;
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



    void Update()
    {
        if(mInvestigationPoints.Count >= 5)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray aRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit aHit;
            if (Physics.Raycast(aRay, out aHit, 1000))
            {
                NavMeshHit aClosestNMP;
                if(NavMesh.SamplePosition(aHit.point, out aClosestNMP, 100,NavMesh.AllAreas))
                {
                    mInvestigationPoints.Add(aClosestNMP.position);
                }
            }
        }
    }



}
