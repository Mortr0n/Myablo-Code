using UnityEngine;

public class DeadState : EnemyStateBase
{
    public DeadState()
    {
        // Debug.Log("I'm in a dead state!");
    }
    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        //ai.UnAlive(); 
        //ai.TriggerDeath();
        // Trigger death animation
        EventsManager.instance.onExperienceGranted.Invoke(ai.ExperienceValue);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: maybe remove or destroy object after a bit using Coroutine?
    }
}
