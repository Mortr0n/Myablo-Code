using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HealSelfEquippableAbility : EquippableAbility
{
    float manaCost = 10f;
    float healWaitTime = 1f;
    bool canCastHeal = true;
    public override void LevelUp()
    {
        //if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        if (PlayerCharacterSheet.instance.TryToSpendSkillPoint(this))
        {
            int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);
            float healMod = 0.1f + 0.05f * skillLevel;
            PlayerController.instance.Combat().SetHealMod(healMod);
        }
    }

    public override void RunAbilityClicked(PlayerController player, float waitTime)
    {
        playerController = player; // this is the player that clicked the ability
        RaycastHit hit = new RaycastHit(); // just need something to pass in it's unused on this
        SuccessfulRaycastFunctionality(ref hit);
        
    }

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        if ( CanCastHeal())
        {
            AudioManager.instance.PlayMagicTwinkleSFX();
            SpawnEquippedAttack(playerController.transform.position);
        }
       
    }

    public bool CanCastHeal()
    {
        // Check both if player has enough mana and if heal wait timer is done
        //Debug.Log($"Can Cast Heal: {myPlayer.Combat().GetMana()} >= {manaCost} && {canCastHeal}");
        return (playerController.Combat().GetMana() >= manaCost) && canCastHeal;
    }

    public IEnumerator HealWaitTimer()
    {
        canCastHeal = false;
        yield return new WaitForSeconds(healWaitTime);
        canCastHeal = true;
    }


    protected override void SpawnEquippedAttack(Vector3 location)
    {
        playerController.Combat().SpendMana(manaCost);
        StartCoroutine(HealWaitTimer());
        Debug.Log($"Healing for {playerController.Combat().GetHealAmount()}");
        if (spawnablePrefab == null)
        {
            Debug.LogError("Heal Prefab is null");
            return;
        } else
        {
            //Debug.Log($"Heal Prefab : {spawnablePrefab}");
            GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
        }


        
        playerController.Combat().Heal(playerController.Combat().GetHealAmount());
        //could also do 
        //float healAmt = myPlayer.Combat().GetHealAmount() * -1;
        //newAttack.GetComponent<HealSelfCA>().InitializeDamage(healAmt);
    }
}
