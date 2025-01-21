using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PursuingState : EnemyStateBase 
{
    GameObject targetToPursue;
    public PursuingState(GameObject targetToPursue)
    {
        this.targetToPursue = targetToPursue;
    }

    public override void Enter(EnemyAI ai)
    {
        if (!ai.IsAlive())
        {
            Debug.Log("Enter and !ai.IsAlive");
            return;
        }
        Debug.Log("Entering Pursuing state");
        if (ai.IsAlive())
        {
            Debug.Log("Enter and ai.IsAlive");
            base.Enter(ai);
            // fill in pursuit here
            RunPursuing(ai);
        }
       
    }


    public override void Update(EnemyAI ai)
    {
        if (!ai.IsAlive())
        {
            return;
        }

        if (ai.TargetIsInAttackRange(targetToPursue))
        {
            if (ai.IsAlive())
            {
                ai.ChangeState(new AttackingState(targetToPursue));
            }
            else if (ai.IsAlive())
            {
                RunPursuing(ai);
            }
        }
            
    }


    void RunPursuing(EnemyAI ai) 
    {
        Debug.Log("RunPursuing");
        if (!ai.IsAlive())
        {
            Debug.Log("RunPursuing and !ai.IsAlive");
            return;
        }

        Debug.Log("Running Pursuing");

        if (targetToPursue == null && ai.IsAlive())
        {
            Debug.Log("RunPursuing and ai.IsAlive targetToPursue");
            ai.TriggerWandering();
            return;
        }
        // go to targets position
        if (ai.IsAlive())
        {
            Debug.Log($"RunPursuing and ai.IsAlive {ai.IsAlive()}");
            if (ai.Agent.navMeshOwner)
            {
                ai.Agent.destination = targetToPursue.transform.position;
            }
            

            if (ai.TargetIsInAttackRange(targetToPursue))
            {
                ai.TriggerAttacking(targetToPursue);
            }
            else if (ai.TargetIsOutofPursuitRange(targetToPursue))
            {
                ai.TriggerWandering();
            }
        }
    }

    //private bool TargetIsOutofPursuitRange(SkeletonFSMAI ai)
    //{
    //    return Vector3.Distance(ai.transform.position, targetToPursue.transform.position) > ai.MaxPursuitDistance;
    //}

    //private bool TargetIsInAttackRange(SkeletonFSMAI ai)
    //{
    //    return Vector3.Distance(ai.transform.position, targetToPursue.transform.position) <= ai.AttackRange;
    //}
}
