using UnityEngine;
using System.Collections;


public class MagicShieldEquippableAbility : EquippableAbility
{
    float manaCost = 10f;
    float shieldWaitTime = 10f;
    bool canCastShield = true;

    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        {
            skillLevel++;
            float shieldMod = 10f  * skillLevel;
            PlayerController.instance.Combat().SetShieldSize(shieldMod);
        }
    }

    public override void RunAbilityClicked(PlayerController player)
    {
        myPlayer = player; // this is the player that clicked the ability
        RaycastHit hit = new RaycastHit(); // just need something to pass in it's unused on this
        SuccessfulRaycastFunctionality(ref hit);
    }

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        if (CanCastShield())
        {
            SpawnEquippedAttack(myPlayer.transform.position);
        }
    }

    public bool CanCastShield()
    {
        // Check both if player has enough mana and if shield wait timer is done
        return (myPlayer.Combat().GetMana() >= manaCost) && canCastShield;
    }

    public IEnumerator ShieldWaitTimer()
    {
        canCastShield = false;
        yield return new WaitForSeconds(shieldWaitTime);
        canCastShield = true;
    }


    protected override void SpawnEquippedAttack(Vector3 location)
    {
        myPlayer.Combat().SpendMana(manaCost);
        
        Debug.Log($"Shielding for {myPlayer.Combat().GetCurrentShield()}");
        if (spawnablePrefab == null)
        {
            Debug.LogError("Shield Prefab is null");
        }
        else if (canCastShield)
        {
            //GameObject newShield = Instantiate(spawnablePrefab, myPlayer.transform.position, Quaternion.identity);
            PlayerController.instance.SetShieldActive();
            //newShield.GetComponent<MagicShieldAbility>().SetFactionID(myPlayer.GetFactionID());
            PlayerController.instance.Combat().SetCurrentShield(PlayerController.instance.Combat().GetShieldSize());
            Debug.Log($"Shielding for {myPlayer.Combat().GetCurrentShield()} {PlayerController.instance.Combat().GetShieldSize()}");
        }
        StartCoroutine(ShieldWaitTimer());
    }


    
}
