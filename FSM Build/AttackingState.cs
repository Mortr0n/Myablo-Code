using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AttackingState : EnemyStateBase
{
    protected GameObject targetToAttack;
    protected NavMeshAgent agent;
    private int atkCount = 0;

    //
    public AttackingState(GameObject target)
    {
        
        this.targetToAttack = target;
    }
    public override void Enter(EnemyAI ai)
    {
        if (!ai.IsAlive())
        {
            return;
        } 
        base.Enter(ai);
        // start animation?  maybe animation would be in update?

        // Keeps the enemies from walking through the player and pushing him around
        if (agent != null)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }
        }

        if (agent == null)
        {
            agent = ai.GetComponent<NavMeshAgent>();
        }
        
        RunAttacking(ai);

    }
    
    public override void Update(EnemyAI ai)
    {
        //Debug.Log($"alive? {ai.IsAlive()}");
        if (!ai.IsAlive())
        {
            return;
        }
        if (!ai.TargetIsInAttackRange(targetToAttack))
        {
            ai.ChangeState(new PursuingState(targetToAttack));
        }
        RunAttacking(ai);
    }


    void RunAttacking(EnemyAI ai)
    {

        // Swing every attack 
        ai.AttackCooldownTimer += Time.deltaTime;

        if (ai.AttackCooldownTimer >= ai.AttackCooldown || ai.AttackCount == 0)
        {
            ai.AttackCountInc();

            ai.GetComponent<EnemyAnimator>().TriggerAttack();

            SpawnAttackPrefab(ai);
            ai.AttackCooldownTimer -= ai.AttackCooldown;
        }

        // if target out of range pursue
        if (!ai.TargetIsInAttackRange(targetToAttack))
        {
            ai.TriggerPursuing(ai.Target);
        }
    }

    void SpawnAttackPrefab(EnemyAI ai)
    {
        //Debug.Log("Spawning attack Prefab");
        //Debug.Log("Attack Prefab spawned");
        Vector3 attackDirection = (ai.Target.transform.position - ai.Agent.transform.position);
        Vector3 spawnPosition = (attackDirection.normalized * ai.AttackRange) + ai.Agent.transform.position;

        GameObject newAttack = Object.Instantiate(ai.AttackPrefab, spawnPosition, Quaternion.identity);
        CombatActor ca = newAttack.GetComponent<CombatActor>();
        ca.SetFactionID(ai.FactionID);
        ca.InitializeDamage(ai.Damage);

    }
}
