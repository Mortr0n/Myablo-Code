using UnityEngine;

public class SpiderQueenAI : EnemyAI
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // SpiderQueen-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // SpiderQueen-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // SpiderQueen-specific state change logic
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        
        // Use SpiderQueen-specific wandering state
        ChangeState(new SpiderQueenWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;

        target = targetToPursue;
        // Use SpiderQueen-specific pursuing state
        ChangeState(new SpiderQueenPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;

        target = targetToAttack;
        // Use SpiderQueen-specific attacking state
        ChangeState(new SpiderQueenAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // SpiderQueen-specific trigger logic
    }
}
