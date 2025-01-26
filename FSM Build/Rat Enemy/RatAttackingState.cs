using UnityEngine;

public class RatAttackingState : AttackingState
{
    

    public RatAttackingState(GameObject target) : base(target)
    {
        this.targetToAttack = target;
    }

    public override void Enter(EnemyAI ai)
    {
        Debug.Log($"Rat Attacking {targetToAttack}");
        base.Enter(ai); 
        // Rat-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Rat-specific attack logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Rat-specific exit behavior
    }
} 