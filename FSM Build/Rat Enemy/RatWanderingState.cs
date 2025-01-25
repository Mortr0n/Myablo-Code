using UnityEngine;

public class RatWanderingState : WanderingState
{
    public RatWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
    {
        // Rat-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Rat-specific enter behavior
        
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Rat-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Rat-specific exit behavior
    }
} 