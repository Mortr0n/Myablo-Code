using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class PursuingState : EnemyStateBase
{
    protected GameObject target;
    protected float runSpeedMultiplier = 2f;
    protected float originalSpeed;
    protected NavMeshAgent agent;

    public PursuingState(GameObject target)
    {
        this.target = target;
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        
        // Cache and modify NavMeshAgent
        agent = ai.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            originalSpeed = agent.speed;
            agent.speed *= runSpeedMultiplier;
        }
        
        // Turns enemy movement on as it gets disabled in other states to avoid pushing the player around
        if (agent != null)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
        }
        // Set running animation
        BasicAnimator animator = ai.GetComponent<BasicAnimator>();
        if (animator != null)
        {
            animator.SetWalking(false);
            animator.SetRunning(true);
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
            agent.SetDestination(target.transform.position);
        }

        float distanceToTarget = Vector3.Distance(ai.transform.position, target.transform.position);
        //Debug.Log($"Dist to targ: {distanceToTarget} and {distanceToTarget < ai.AttackRange} and range {ai.AttackRange}");
        if (agent == null)
        {
            agent = ai.GetComponent<NavMeshAgent>();
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
        if (agent != null)
        {
            agent.speed = originalSpeed;
        }

        // Reset animation state
        BasicAnimator animator = ai.GetComponent<BasicAnimator>();
        if (animator != null)
        {
            animator.SetRunning(false);
        }
        
        base.Exit(ai);
    }
}
