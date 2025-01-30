using UnityEngine;

public class MiniHellWatcherPursuingState : PursuingState
{
    public MiniHellWatcherPursuingState(GameObject target) : base(target)
    {
        // MiniHellWatcher-specific initialization if needed
        runSpeedMultiplier = 2.5f;
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // MiniHellWatcher-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // MiniHellWatcher-specific pursuing logic
        ai.speed = agent.velocity.magnitude;
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // MiniHellWatcher-specific exit behavior
    }
}
