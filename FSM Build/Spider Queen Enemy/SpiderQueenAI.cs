using UnityEngine;

public class SpiderQueenAI : EnemyAI
{
    private bool isSleeping = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

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
        Debug.Log($"SpiderQueen Pursuit {targetToPursue} and base target {target} ");
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

    public override void TriggerEntering(GameObject targetObject)
    {
        //BasicAnimator animator = GetComponent<BasicAnimator>();
        //animator.SetEntering(true);
        base.RunAI();
        
        isSleeping = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other == null || other.gameObject == null) return;
        
        if (!isSleeping) 
        { 
            //base.OnTriggerEnter(other);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            BasicAnimator animator = GetComponent<BasicAnimator>();
            if (animator != null)
            {
                animator.SetEntering(true);
            }
            TriggerEntering(other.gameObject);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (!isSleeping && other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
        }
        //else if ()
        //{
        //    TriggerEntering(other.gameObject);
        //}
    }
}
