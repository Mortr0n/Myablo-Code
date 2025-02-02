using UnityEngine;

public class GhoulWanderingState : WanderingState
{
    public GhoulWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
    {
        // Ghoul-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Ghoul-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Ghoul-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Ghoul-specific exit behavior
    }
}
