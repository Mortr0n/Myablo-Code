using UnityEngine;
using System.Collections;


public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;


    public virtual void RunAbilityClicked(PlayerController player, float abilityCooldown)
    {
        SkillCooldownUI skillCooldownUI = FindFirstObjectByType<SkillCooldownUI>();
        if (MouseIsOverUI()) return;

        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out hit))
        {
            if (abilityCooldown > 0) StartCoroutine(skillCooldownUI.SkillCooldown(abilityCooldown));
            SuccessfulRaycastFunctionality(ref hit);
        }
    }

    public override void LevelUp()
    {
        //if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        if (PlayerCharacterSheet.instance.TryToSpendSkillPoint(this)) Debug.Log($"Skill Spend successful I guess");
    }

    protected virtual void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        myPlayer.Movement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<Clickable>())
        {
            targetedReceiver = hit.collider.GetComponent<CombatReceiver>();
        }
    }

    protected virtual float ReturnCritChance(float dex)
    {
        // utilizing Sigmoid formula for curve control on crit chance ramp up.
        float maxCritChance = 1f;
        float growthFactor = .04f;
        float midPoint = 40f;
        float critChance = maxCritChance / (1 + Mathf.Exp(-growthFactor * (dex - midPoint)));

        return critChance * 100;
    }

    protected virtual float ReturnCritMod(float strBonus, float playerStrength)
    {
        float playerDex = PlayerCharacterSheet.instance.GetDexterity();
        float dexBonus = 2 * (playerDex - 10);

        float critMod = 1;
        float critChance = ReturnCritChance(playerDex);


        int random = Random.Range(0, 100);
        if (random < critChance) critMod = 1 + dexBonus + strBonus;
        Debug.Log($"Dexbonus: {dexBonus} Crit chance: {critChance} random: {random} critMod: {critMod}: Did it crit? {random < critChance}");
        return critMod;
    }

    public float GetMeleeeDamage()
    {
        float playerStrength = PlayerCharacterSheet.instance.GetStrength();

        float strBonus = (playerStrength - 15);
        float critMod = ReturnCritMod(strBonus, playerStrength);

        float calculatedDamage = (playerStrength / 2) * critMod;
        return calculatedDamage;
    }

    protected virtual void SpawnEquippedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());
        
        //FIXME: Need to update this to not be as big maybe start at 1.2 and go up from there
        float calculatedDamage = GetMeleeeDamage();

        Debug.Log($"calculated damage: {calculatedDamage}");
        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }

    protected virtual void Update()
    {
        if (targetedReceiver != null)   RunTargetAttack();
    }


    protected virtual void RunTargetAttack()
    {
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
