using UnityEngine;

public class MiniHellWatcherAttackingState : AttackingState
{
    public MiniHellWatcherAttackingState(GameObject target) : base(target)
    {
        // MiniHellWatcher-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // MiniHellWatcher-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // MiniHellWatcher-specific attacking logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // MiniHellWatcher-specific exit behavior
    }
} 