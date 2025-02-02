using UnityEngine;

public class WerewolfAI : EnemyAI
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Werewolf-specific initialization
    }

    protected override void RunAI()
    {
        base.RunAI();
        // Werewolf-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
        // Werewolf-specific state change logic
    }

    public override void TriggerWandering()
    {
        Debug.Log("Wandering triggered");
        if (!aiAlive) return;
        
        // Use Werewolf-specific wandering state
        ChangeState(new WerewolfWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive) return;
        Debug.Log("Pursuing triggered");
        target = targetToPursue;
        // Use Werewolf-specific pursuing state
        ChangeState(new WerewolfPursuingState(targetToPursue));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive) return;
        Debug.Log("Attacking triggered");
        target = targetToAttack;
        // Use Werewolf-specific attacking state
        ChangeState(new WerewolfAttackingState(target));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        // Werewolf-specific trigger logic
    }
}
