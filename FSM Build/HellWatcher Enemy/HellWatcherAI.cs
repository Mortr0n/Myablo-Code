using UnityEngine;

public class HellWatcherAI : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        // HellWatcher-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // HellWatcher-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // HellWatcher-specific state change logic
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use HellWatcher-specific wandering state
        ChangeState(new HellWatcherWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use HellWatcher-specific pursuing state
        ChangeState(new HellWatcherPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use HellWatcher-specific attacking state
        ChangeState(new HellWatcherAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // HellWatcher-specific trigger logic
    }
}
