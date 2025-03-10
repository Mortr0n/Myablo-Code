using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : BasicAI
{
    private EnemyStateBase currentState;

    public MonoBehaviour monoBehaviour;
    // Pursuing State Vars
    [SerializeField] protected GameObject target;
    [SerializeField] protected float maxWanderDistance = 6;
    [SerializeField] protected float maxPursuitDistance = 15f;
    [SerializeField] protected float attackRange = 2;
    protected Vector3 startPosition = Vector3.zero;
    [SerializeField] protected bool sleeping;
    [SerializeField] protected int attackCount = 0;
    // Attacking State Vars
    [SerializeField] protected float damage = 3;
    [SerializeField] protected float attackCooldown = 2.5f;
    protected float attackCooldownTimer = 0.0f;
    [SerializeField] protected GameObject attackPrefab;
    [SerializeField] protected int enemyLevel = 1;
    [SerializeField] protected float experienceValue = 45;
    

    //NOTE: this is for debugging animations and is not to be used for anything other than seeing them in the inspector!!!
    [SerializeField] public float speed;
    //public UnityEngine.AI.NavMeshAgent Agent
    //{
    //    get { return agent; }
    //}
    public int EnemyLevel { get { return enemyLevel; } set { enemyLevel = value; } }

    public GameObject Target { get { return target; } }

    //public UnityEngine.AI.NavMeshAgent Agent { get { return agent; } }
    // Pursuing Props
    public float MaxWanderDistance { get { return maxWanderDistance; } }
    public float MaxPursuitDistance { get { return maxPursuitDistance; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public Vector3 StartPosition { get { return startPosition; } }

    // Attacking Props
    public float Damage { get { return damage; } }
    public float AttackCooldown { get { return attackCooldown; } }
    public float AttackCooldownTimer { get { return attackCooldownTimer; } set { attackCooldownTimer = value; } }
    public int AttackCount { get { return attackCount; } }
    public GameObject AttackPrefab { get { return attackPrefab; }  }
    public float ExperienceValue { get { return experienceValue; } }

    public void InitializeEnemy()
    {
        experienceValue = 45 * enemyLevel;
        damage = damage * enemyLevel;
        attackCooldown = attackCooldown * (1 - (.02f * enemyLevel * 10f ));
    }

    protected virtual void Start()
    {
        //Debug.Log("Start FSMAI");
        startPosition = transform.position;
        ChangeState(new WanderingState(maxWanderDistance, startPosition));
    }

    public virtual  void AttackCountInc()
    {
        attackCount++;
        return;
    }

    public virtual void ResetAttackCount()
    {
        attackCount = 0;
    }

    protected override void RunAI()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
    }
    public void SetAttackRange(float range)
    {
        AttackRange = range;
    }
    
    protected override void Update() 
    {
        base.Update();
        speed = agent.velocity.magnitude;
    }

    public bool GetSleeping()
    {
        return sleeping;
    }

    public void SetSleeping(bool sleepSet)
    {
        sleeping = sleepSet;
    }

    public virtual void ChangeState(EnemyStateBase newState)
    {
        if (currentState != null && newState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public virtual void TriggerWandering()
    {
        if (!aiAlive)
        {
            return;
        }

        ChangeState(new WanderingState(maxWanderDistance, startPosition));

    }

    public virtual void TriggerPursuing(GameObject targetToPursue)
    {
        if (!aiAlive)
        {
            return;
        }
        this.target = targetToPursue;
        ChangeState(new PursuingState(targetToPursue));
    }

    

    public bool TargetIsOutofPursuitRange(GameObject targetToPursue)
    {
        return Vector3.Distance(transform.position, targetToPursue.transform.position) > maxPursuitDistance;
    }

    public bool TargetIsInAttackRange(GameObject targetToPursue)
    {
        //Debug.Log($"Target in attack range ? {Vector3.Distance(transform.position, targetToPursue.transform.position) <= attackRange}");
        return Vector3.Distance(transform.position, targetToPursue.transform.position) <= attackRange;
    }

    public virtual void TriggerAttacking(GameObject targetToAttack)
    {
        if (!aiAlive)
        {
            return;
        }

        this.target = targetToAttack;
        ChangeState(new AttackingState(target));
    }

    public virtual void TriggerEntering(BasicAnimator animator)
    {
        if (!aiAlive || animator == null) return;

        //target = animator;
        
        ChangeState(new EnteringState(animator));
    }
    

    public override void TriggerDeath()
    {
        if (!aiAlive)
        {
            return;
        }
        base.TriggerDeath();

        ChangeState(new DeadState());
        aiAlive = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"other {other.name} {other.gameObject.name} combat Reciever {other.GetComponent<CombatReceiver>()} not istrigger? {!other.isTrigger}");
        if (other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            //FIXME: I set it to the playercontroller because it wasn't getting the correct factionid utilizing this function
            // Fixed after setting player factionID as serializedfield
            //Debug.Log($"faction: {factionID}  otherID: {other.GetComponent<CombatReceiver>().GetFactionID()}");
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                //Debug.Log($"faction: {factionID}  otherID: {other.GetComponent<CombatReceiver>().GetFactionID()}");
                TriggerPursuing(other.gameObject);
            }
        }
    }
}
