using UnityEngine;

public class WerewolfAttackingState : AttackingState
{
    public WerewolfAttackingState(GameObject target) : base(target)
    {
        // Werewolf-specific initialization if needed
        this.targetToAttack = target;
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Werewolf-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Werewolf-specific attacking logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Werewolf-specific exit behavior
    }
}
