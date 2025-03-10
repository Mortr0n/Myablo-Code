using UnityEngine;

public class RatPursuingState : PursuingState
{
    public RatPursuingState(GameObject target) : base(target)
    {
        // Rats might move faster than other enemies
        runSpeedMultiplier = 2.5f;  // Adjust this value for rat-specific speed
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Any rat-specific enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        
        // Add any rat-specific pursuit behavior here
        // For example, rats might zigzag while pursuing:
        if (agent != null && target != null)
        {
            // Example of rat-specific movement modification
            // Vector3 zigzag = target.transform.position + Random.insideUnitSphere * 2f;
            // zigzag.y = target.transform.position.y;
            // agent.SetDestination(zigzag);
        }
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Any rat-specific exit behavior
    }
} 