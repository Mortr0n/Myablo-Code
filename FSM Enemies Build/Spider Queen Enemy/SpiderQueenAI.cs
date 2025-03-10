using UnityEngine;

public class SpiderQueenAI : EnemyAI
{
    //protected bool sleeping  = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        SetSleeping(true);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetAttackRange(2.5f);

        //NOTE: Turns enemy movement on as it gets disabled in other states to avoid pushing the player around
        if (agent != null)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }
        }
    }

    protected override void RunAI()
    {
        base.RunAI();
        // SpiderQueen-specific AI update logic
    }

    public override void ChangeState(EnemyStateBase newState)
    {
        base.ChangeState(newState);
    }

    public override void TriggerWandering()
    {
        if (!aiAlive) return;
        ChangeState(new SpiderQueenWanderingState(maxWanderDistance, startPosition));
    }

    public override void TriggerPursuing(GameObject targetToPursue)
    {
        //Debug.Log($"SpiderQueen Pursuit {targetToPursue} and base target {target} ");
        if (!aiAlive || targetToPursue == null) return;
        target = targetToPursue;
        ChangeState(new SpiderQueenPursuingState(target));
    }

    public override void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive || targetToAttack == null) return;
        target = targetToAttack;
        ChangeState(new SpiderQueenAttackingState(target));
    }

    public override void TriggerEntering(BasicAnimator animator)
    {
        //isSleeping = false;
        //BasicAnimator animator = GetComponent<BasicAnimator>();
        ChangeState(new SpiderQueenEnteringState(animator));
        //animator.SetEntering(true);
        
        
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other == null || other.gameObject == null) return;
        
        //Debug.Log($"Trigger Ent: {other.gameObject}");
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Debug.Log($"Trigger Stay: {other.gameObject}");
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
        }

    }
}
