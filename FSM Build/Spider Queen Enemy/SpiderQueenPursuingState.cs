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
