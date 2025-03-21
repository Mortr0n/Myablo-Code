using UnityEngine;

public class TeslaEquippableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 5;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastLightning(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            playerController.Movement().MoveToLocation(playerController.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayElectricLightningSFX();
            playerController.Combat().SpendMana(manaCost);
        }
        else
        {
            playerController.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastLightning(ref RaycastHit hit)
    {
        return playerController.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {

        playerController.transform.LookAt(new Vector3(location.x, playerController.transform.position.y, location.z));

        Vector3 spawnPosition = playerController.transform.position + playerController.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<LightningCA>().SetFactionID(playerController.GetFactionID());
        newAttack.GetComponent<LightningCA>().SetShootDirection(playerController.transform.forward);

        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);

        float calculatedDamage = .25f + (.25f * skillLevel);
        newAttack.GetComponent<LightningCA>().InitializeDamage(calculatedDamage, playerController.gameObject);
    }
}
