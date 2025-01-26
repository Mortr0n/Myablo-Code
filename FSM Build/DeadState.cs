using UnityEngine;
using UnityEngine.AI;

public class DeadState : EnemyStateBase
{
    protected NavMeshAgent agent;
    public DeadState()
    {
        // Debug.Log("I'm in a dead state!");
    }
    public override void Enter(EnemyAI ai)
    {
     
        agent = ai.GetComponent<NavMeshAgent>();

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
