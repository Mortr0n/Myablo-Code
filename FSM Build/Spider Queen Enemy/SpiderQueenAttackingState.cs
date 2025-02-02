using UnityEngine;

public class SpiderQueenAttackingState : AttackingState
{
    public SpiderQueenAttackingState(GameObject target) : base(target)
    {
        // SpiderQueen-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // SpiderQueen-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // SpiderQueen-specific attacking logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // SpiderQueen-specific exit behavior
    }
}
