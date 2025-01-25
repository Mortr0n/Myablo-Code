using UnityEngine;

public class RatAttackingState : EnemyStateBase
{
    protected GameObject target;

    public RatAttackingState(GameObject target)
    {
        this.target = target;
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Rat-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Rat-specific attack logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Rat-specific exit behavior
    }
} 