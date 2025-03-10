using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpiderQueenWanderingState : WanderingState
{
    private bool isSleeping = true;
    MonoBehaviour myMonoBehaviour;
    public SpiderQueenWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
    {
        // SpiderQueen-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        agent = ai.GetComponent<NavMeshAgent>();

        //NOTE: Turns enemy movement on as it gets disabled in other states to avoid pushing the player around
        if (agent != null)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
        }

        // Set walking animation
        BasicAnimator animator = ai.GetComponent<BasicAnimator>();
        if (animator != null)
        {
            animator.SetEntering(true);
            
        }

        if (!isSleeping)
        {
            GetNewWanderDestination(ai);
        } else
        {
            myMonoBehaviour.StartCoroutine(DelayWandering(ai, animator));
        }
        //base.Enter(ai);
        // SpiderQueen-specific enter behavior
    }

    public IEnumerator DelayWandering(EnemyAI ai, BasicAnimator animator)
    {
        yield return new WaitForSeconds(.2f);

        if (animator != null)
        {
            animator.SetEntering(false);
            animator.SetRunning(false);
            animator.SetWalking(true);
        }

        GetNewWanderDestination(ai);
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // SpiderQueen-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // SpiderQueen-specific exit behavior
    }
}
