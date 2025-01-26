using UnityEngine;

public class BoarAttackingState : AttackingState
{
    public BoarAttackingState(GameObject target) : base(target)
    {
        // Boar-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Boar-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Boar-specific attacking logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Boar-specific exit behavior
    }
}
