using UnityEngine;

public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;

    public virtual void RunAbilityClicked(PlayerController player)
    {
        if (MouseIsOverUI()) return;

        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out hit))
        {
            SuccessfulRaycastFunctionality(ref hit);
        }
    }

    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
            skillLevel++;
    }

    protected virtual void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        myPlayer.Movement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<Clickable>())
        {
            //Debug.Log("Clickable clicked");
            targetedReceiver = hit.collider.GetComponent<CombatReceiver>();
            //targetedReceiver = hit.collider.gameObject.GetComponent<CombatReceiver>(); //TODO: Original way
        }
    }

    protected virtual void SpawnEquippedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        //Debug.Log($"!!!! {this.name} newAttack: {newAttack} prefab: {spawnablePrefab.name}");
        newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());

        float critMod = 1;
        int random = Random.Range(0, 100);
        float playerDex = PlayerCharacterSheet.instance.GetDexterity();
        if (random < playerDex) critMod = 2;

        float playerStrength = PlayerCharacterSheet.instance.GetStrength();
        float calculatedDamage = (playerStrength / 5) * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }

    protected virtual void Update()
    {
        if (targetedReceiver != null)   RunTargetAttack();

        //if (Input.GetKeyDown(KeyCode.Space)) 
        //{
        //    Dash();
        //}
    }
    //protected void Dash()
    //{
    //    myPlayer = player;
    //    Debug.Log($"myPlayer: {myPlayer}");
    //    Vector3 dashDirection = myPlayer.Movement().GetLastMovementDirection();

    //    // Prevent dashing in place
    //    if (dashDirection == Vector3.zero) return;

    //    myPlayer.Movement().PerformDash(dashDirection);
    //}

    protected virtual void RunTargetAttack()
    {
        //Debug.Log("Run Target Attack");
        if (Vector3.Distance(myPlayer.transform.position, targetedReceiver.transform.position) <= attackRange)
        {
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);

            Vector3 lookPos = new Vector3(targetedReceiver.transform.position.x, myPlayer.transform.position.y, targetedReceiver.transform.position.z);
            myPlayer.transform.LookAt(lookPos);

            SpawnEquippedAttack(myPlayer.transform.position + myPlayer.transform.forward);
            myPlayer.GetAnimator().TriggerAttack();


            targetedReceiver = null;
        }
        else
        {
            myPlayer.Movement().MoveToLocation(targetedReceiver.transform.position);
        }
    }

    public bool MouseIsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
