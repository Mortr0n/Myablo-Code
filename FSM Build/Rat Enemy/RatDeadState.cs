public class RatDeadState : EnemyStateBase
{
    public RatDeadState()
    {
        // Rat-specific initialization
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        // Rat-specific death enter behavior
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);
        // Rat-specific death update logic
    }

    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);
        // Rat-specific death exit behavior
    }
} 