using UnityEngine;

public class HellWatcherPursuingState : PursuingState
{
    public HellWatcherPursuingState(GameObject target) : base(target)
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
        // HellWatcher-specific pursuing logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // HellWatcher-specific exit behavior
    }
}
