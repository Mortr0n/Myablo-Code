using UnityEngine;

public class RatAI : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        // Rat-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // Rat-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        // if(!aiAlive) return;
        base.ChangeState(newState);
        // Rat-specific state change logic
        // ChangeState(newState);s
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use rat-specific wandering state
        ChangeState(new RatWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use rat-specific pursuing state
        ChangeState(new RatPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use rat-specific attacking state
        ChangeState(new RatAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Rat-specific trigger logic
    }
}
