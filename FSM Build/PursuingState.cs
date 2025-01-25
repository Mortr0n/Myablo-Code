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
        if (distanceToTarget <= ai.AttackRange)
        {
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
