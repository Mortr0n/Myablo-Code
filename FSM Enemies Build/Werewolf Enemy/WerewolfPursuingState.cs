using UnityEngine;

public class WerewolfPursuingState : PursuingState
{
    public WerewolfPursuingState(GameObject target) : base(target)
    {
        this.target = target;
        runSpeedMultiplier = 3.5f;
        // Werewolf-specific initialization if needed
        Debug.Log("In Pursue");
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Werewolf-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Werewolf-specific pursuing logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Werewolf-specific exit behavior
    }
}
