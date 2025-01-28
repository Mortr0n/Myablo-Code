using UnityEngine;

public class MiniHellWatcherWanderingState : WanderingState
{
    public MiniHellWatcherWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
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
        // MiniHellWatcher-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // MiniHellWatcher-specific exit behavior
    }
}
