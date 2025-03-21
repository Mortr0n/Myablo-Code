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
        // Turns enemy movement on as it gets disabled in other states to avoid pushing the player around
        agent = ai.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            originalSpeed = agent.speed;
            agent.speed *= runSpeedMultiplier;
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
            ai.TriggerWandering();
            return;
        }

        if (agent != null)
        {
            agent.SetDestination(target.transform.position);
        }

        float distanceToTarget = Vector3.Distance(ai.transform.position, target.transform.position);
        if (agent == null)
        {
            agent = ai.GetComponent<NavMeshAgent>();
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
            ai.TriggerAttacking(target);
        }
        else if (distanceToTarget > ai.MaxPursuitDistance)
        {
            ai.TriggerWandering();
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
