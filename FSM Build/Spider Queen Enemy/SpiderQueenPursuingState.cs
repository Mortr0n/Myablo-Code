using UnityEngine;

public class SpiderQueenPursuingState : PursuingState
{
    public SpiderQueenPursuingState(GameObject target) : base(target)
    {
        // SpiderQueen-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // SpiderQueen-specific enter behavior
        agent = ai.GetComponent<UnityEngine.AI.NavMeshAgent>();

        //NOTE: Turns enemy movement on as it gets disabled in other states to avoid pushing the player around
        if (agent != null)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
        }
    }

    public override void Update(EnemyAI ai)
    {
        if (target == null)
        {
            //Debug.LogWarning($"Target is null, returning to wandering state t: {target} ");
            ai.TriggerWandering();
            return;
        }

        if (agent != null)
        {
            agent.stoppingDistance = 2.5f;
            agent.SetDestination(target.transform.position);
        }

        float distanceToTarget = Vector3.Distance(ai.transform.position, target.transform.position);
        //Debug.Log($"Dist to targ: {distanceToTarget} and {distanceToTarget < ai.AttackRange} and range {ai.AttackRange}");
        if (agent == null)
        {
            agent = ai.GetComponent<UnityEngine.AI.NavMeshAgent>();
            //Debug.Log($"agent {agent} isStopped {agent.isStopped}");
        }

        if (distanceToTarget <= ai.AttackRange)
        {
            if (agent != null)
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }
            }
            //agent.SetDestination(agent.transform.position);
            ai.TriggerAttacking(target);
        }
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // SpiderQueen-specific exit behavior
    }
}
