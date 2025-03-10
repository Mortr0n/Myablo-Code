using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyStateBase
{
    public virtual void Enter(EnemyAI ai) { }
    public virtual void Exit(EnemyAI ai) { }
    public virtual void Update(EnemyAI ai) { }

}