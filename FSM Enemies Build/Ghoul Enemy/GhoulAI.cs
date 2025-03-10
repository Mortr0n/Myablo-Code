using UnityEngine;

public class GhoulAI : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        // Ghoul-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // Ghoul-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // Ghoul-specific state change logic
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use Ghoul-specific wandering state
        ChangeState(new GhoulWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use Ghoul-specific pursuing state
        ChangeState(new GhoulPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use Ghoul-specific attacking state
        ChangeState(new GhoulAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Ghoul-specific trigger logic
    }
}
