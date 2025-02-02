using UnityEngine;

public class WerewolfWanderingState : WanderingState
{
    public WerewolfWanderingState(float maxWanderDistance, Vector3 startPosition) 
        : base(maxWanderDistance, startPosition)
    {
        // Werewolf-specific initialization if needed
        Debug.Log("In Wander");
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Werewolf-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        ai.speed = agent.velocity.magnitude;
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Werewolf-specific exit behavior
    }
}
