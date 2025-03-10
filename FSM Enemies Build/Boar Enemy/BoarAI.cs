using UnityEngine;

public class BoarAI : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        // Boar-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // Boar-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // Boar-specific state change logic
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use boar-specific wandering state
        ChangeState(new BoarWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use boar-specific pursuing state
        ChangeState(new BoarPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use boar-specific attacking state
        ChangeState(new BoarAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Boar-specific trigger logic
    }
}
