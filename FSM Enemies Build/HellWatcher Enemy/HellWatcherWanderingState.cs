using UnityEngine;

public class HellWatcherWanderingState : WanderingState
{
    public HellWatcherWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
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
        // HellWatcher-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // HellWatcher-specific exit behavior
    }
}
