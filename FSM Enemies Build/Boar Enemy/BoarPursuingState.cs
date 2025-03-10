using UnityEngine;

public class BoarPursuingState : PursuingState
{
    public BoarPursuingState(GameObject target) : base(target)
    {
        // Boar-specific initialization if needed
        runSpeedMultiplier = 2.5f;
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Boar-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Boar-specific pursuing logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Boar-specific exit behavior
    }
}
