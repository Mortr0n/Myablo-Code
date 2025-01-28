using UnityEngine;

public class MiniHellWatcherAI : EnemyAI
{
    protected override void Start()
    {
        base.Start();
        // MiniHellWatcher-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // MiniHellWatcher-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // MiniHellWatcher-specific state change logic
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use MiniHellWatcher-specific wandering state
        ChangeState(new MiniHellWatcherWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use MiniHellWatcher-specific pursuing state
        ChangeState(new MiniHellWatcherPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use MiniHellWatcher-specific attacking state
        ChangeState(new MiniHellWatcherAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // MiniHellWatcher-specific trigger logic
    }
}
