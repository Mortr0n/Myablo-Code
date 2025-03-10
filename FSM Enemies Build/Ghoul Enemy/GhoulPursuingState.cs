using UnityEngine;

public class GhoulPursuingState : PursuingState
{
    public GhoulPursuingState(GameObject target) : base(target)
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
        // Ghoul-specific pursuing logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Ghoul-specific exit behavior
    }
}
