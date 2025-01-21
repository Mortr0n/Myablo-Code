using UnityEngine;

public class WanderingState : EnemyStateBase
{
    float maxWanderDistance;
    Vector3 startPosition;

    public WanderingState(float maxWanderDistance, Vector3 startPosition)
    {
        this.maxWanderDistance = maxWanderDistance;
        this.startPosition = startPosition;
    }

    public override void Enter(EnemyAI ai)
    {
        //Debug.Log("Enter Wandering");
        base.Enter(ai);
        GetNewWanderDestination(ai);
    }

    void GetNewWanderDestination(EnemyAI ai)
    {
        //Debug.Log("Getting new wander destination");
        float randomX = Random.Range(-maxWanderDistance, maxWanderDistance);
        float randomZ = Random.Range(-maxWanderDistance, maxWanderDistance);
        float x = randomX + startPosition.x;
        float y = startPosition.y;
        float z = randomZ + startPosition.z;

        ai.Agent.destination = new Vector3(x, y, z);
        //Debug.Log($"new destination {ai.Agent.destination}");
    }
    public override void Update(EnemyAI ai)
    {
        // TODO:  Look to update state in order to chase player so ai.ChangeState(new PursuingState());
        RunWandering(ai);
    }

    void RunWandering(EnemyAI ai)
    {

        float x = ai.Agent.destination.x;
        float y = ai.transform.position.y;
        float z = ai.Agent.destination.z;

        Vector3 checkPosition = new Vector3(x, y, z);

        if (Vector3.Distance(ai.transform.position, checkPosition) < 1)
        {
            GetNewWanderDestination(ai);
        }
    }
}
