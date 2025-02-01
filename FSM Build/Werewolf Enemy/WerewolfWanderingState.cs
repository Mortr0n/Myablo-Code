using UnityEngine;

public class WerewolfWanderingState : WanderingState
{
    public WerewolfWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
    {
        // Werewolf-specific initialization if needed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Werewolf-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Werewolf-specific wandering logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Werewolf-specific exit behavior
    }
}
