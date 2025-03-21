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

        if (agent == null)
        {
            agent = ai.GetComponent<NavMeshAgent>();
        }

        // Keeps the enemies from walking through the player and pushing him around
        if (agent != null)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }
        }
    }
    
    public override void Update(EnemyAI ai)
    {
        if (!ai.IsAlive())
        {
            return;
        }
        //Debug.Log($"targetToAttack: {targetToAttack}");
        if (targetToAttack != null && !ai.TargetIsInAttackRange(targetToAttack))
        {
            //Debug.Log("Target out of range or null");
            ai.ChangeState(new PursuingState(targetToAttack));
        }
        RunAttacking(ai);
    }


    protected virtual void RunAttacking(EnemyAI ai)
    {
        // Swing every attack 
        ai.AttackCooldownTimer += Time.deltaTime;

        if (ai.AttackCooldownTimer >= ai.AttackCooldown || ai.AttackCount == 0)
        {
            ai.AttackCountInc(); //TODO: If I don't end up using the attack count, remove it

            ai.GetComponent<EnemyAnimator>().TriggerAttack();

            SpawnAttackPrefab(ai);
            ai.AttackCooldownTimer -= ai.AttackCooldown;
        }

        // if target out of range pursue
        if (targetToAttack != null && !ai.TargetIsInAttackRange(targetToAttack))
        {
            //Debug.Log($"Target out of range or null");
            ai.TriggerPursuing(ai.Target);
        } 
        //Debug.Log($"targetToAttack: {targetToAttack}");
        if (targetToAttack == null) ai.TriggerWandering();
    }

    protected virtual void SpawnAttackPrefab(EnemyAI ai)
    {
        Vector3 attackDirection = (ai.Target.transform.position - ai.Agent.transform.position);
        Vector3 spawnPosition = (attackDirection.normalized * ai.AttackRange) + ai.Agent.transform.position;

        GameObject newAttack = Object.Instantiate(ai.AttackPrefab, spawnPosition, Quaternion.identity);
        CombatActor ca = newAttack.GetComponent<CombatActor>();
        ca.SetFactionID(ai.FactionID);
        ca.InitializeDamage(ai.Damage, ai.gameObject);
    }
}
