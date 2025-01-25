using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AttackingState : EnemyStateBase
{
    GameObject targetToAttack;

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
        //Debug.Log("Entering Attacking state");
        if (ai.IsAlive())
        {

            base.Enter(ai);
            // start animation?  maybe animation would be in update?
            RunAttacking(ai);
        }
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
            if (ai.IsAlive())
            {
                ai.ChangeState(new PursuingState(targetToAttack));
            }
           
        } else if(ai.IsAlive())
        {
            RunAttacking(ai);
        }



    }


    void RunAttacking(EnemyAI ai)
    {
        //Debug.Log($"Running Attacking against ai {targetToAttack} timer {ai.AttackCooldownTimer}"); 
        // Swing every attackCD second
        ai.AttackCooldownTimer += Time.deltaTime;

        if (ai.AttackCooldownTimer >= ai.AttackCooldown)
        {
            //Debug.Log("inside attack cooldown");
            ai.AttackCooldownTimer -= ai.AttackCooldown;
            SpawnAttackPrefab(ai);
            ai.GetComponent<EnemyAnimator>().TriggerAttack();
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
        newAttack.GetComponent<CombatActor>().SetFactionID(ai.FactionID);

    }
}
