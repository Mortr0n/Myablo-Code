using UnityEngine;

public class HellWatcherAttackingState : AttackingState
{
    public HellWatcherAttackingState(GameObject target) : base(target)
    {
        // HellWatcher-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // HellWatcher-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // HellWatcher-specific attacking logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // HellWatcher-specific exit behavior
    }
}
