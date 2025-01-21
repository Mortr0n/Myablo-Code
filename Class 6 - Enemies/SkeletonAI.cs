using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : BasicAI
{
    enum SkeletonState { Wandering, Pursuing, Attacking, Dead }
    SkeletonState aiState = SkeletonState.Wandering;

    // Wander state variables
    [SerializeField] float maxWanderDistance = 6;
    Vector3 startPosition = Vector3.zero;

    // Pursuing State Vars
    GameObject target;
    [SerializeField] float maxPursuitDistance = 15f;
    [SerializeField] float attackRange = 1.75f;

    // Attacking State Vars
    [SerializeField] float damage = 3;
    [SerializeField] float attackCooldown = 2.5f;
    float attackCooldownTimer = 0.0f;
    [SerializeField] GameObject attackPrefab;


    // Death Variables
    [SerializeField] float experienceValue = 45;



    private void Start()
    {
        startPosition = transform.position;
        TriggerWandering();

        EventsManager.instance.onPlayerDied.AddListener(TriggerWandering);
    }
    protected override void RunAI()
    {
        // so far 4 different behaviors for the ai state 
        switch (aiState) 
        {
            case SkeletonState.Wandering:
                // run wandering functionality
                RunWandering();
                break;
            case SkeletonState.Pursuing:
                RunPursuing();
                break;

            case SkeletonState.Attacking:
                RunAttacking();
                break;

            case SkeletonState.Dead:
                // he be ded

                break;
        }        

    }

    #region Wandering
    void TriggerWandering()
    {
        aiState = SkeletonState.Wandering;
        GetNewWanderDestination();
    }

    void RunWandering()
    {
        float x = agent.destination.x;
        float y = transform.position.y;
        float z = agent.destination.z;

        Vector3 checkPosition = new Vector3(x, y, z);

        if (Vector3.Distance(transform.position, checkPosition) < 1)
        {
            GetNewWanderDestination();
        }
    }

    void GetNewWanderDestination()
    {
        float randomX = Random.Range(-maxWanderDistance, maxWanderDistance);
        float randomZ = Random.Range(-maxWanderDistance, maxWanderDistance);
        float x = randomX + startPosition.x;
        float y = startPosition.y;
        float z = randomZ + startPosition.z;

        agent.destination = new Vector3(x, y, z);
    }
    #endregion


    #region Pursuing
    void TriggerPursuing(GameObject targetToPursue)
    {
        aiState = SkeletonState.Pursuing;
        target = targetToPursue;
    }

    void RunPursuing()
    {
        if(target == null) 
        {
            TriggerWandering();
            return; 
        }
        // go to targets position
        agent.destination = target.transform.position;

        if (TargetIsInAttackRange())
        {
            TriggerAttacking();
        }
        else if (TargetIsOutofPursuitRange())
        {
            TriggerWandering();
        }
    }

    private bool TargetIsOutofPursuitRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) > maxPursuitDistance;
    }

    private bool TargetIsInAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    #endregion


    #region Attacking
    void TriggerAttacking()
    {
        aiState = SkeletonState.Attacking;
        agent.destination = transform.position;
    }

    void RunAttacking()
    {
        // Swing every attackCD second
        attackCooldownTimer += Time.deltaTime;

        if (attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            GetComponent<EnemyAnimator>().TriggerAttack();
        }

        // if target out of range pursue
        if(!TargetIsInAttackRange())
        {
            TriggerPursuing(target);
        }
    }

    void SpawnAttackPrefab()
    {
        //Debug.Log("Attack Prefab spawned");
        Vector3 attackDirection = (target.transform.position - transform.position);
        Vector3 spawnPosition = (attackDirection.normalized * attackRange) + transform.position;

        GameObject newAttack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);

    }
    #endregion


    #region Dead
    public override void TriggerDeath()
    {
        base.TriggerDeath();

        
        //TODO: Add Experience
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
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
