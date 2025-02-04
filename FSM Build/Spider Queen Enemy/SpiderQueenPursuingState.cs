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
        base.Update(ai);
        // SpiderQueen-specific pursuing logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // SpiderQueen-specific exit behavior
    }
}
