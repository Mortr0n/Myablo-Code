using UnityEngine;

public class FireballEquipableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 5;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastFireball(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            playerController.Movement().MoveToLocation(playerController.transform.position);

            AudioManager.instance.PlayFireballSFX();
            playerController.Combat().SpendMana(manaCost);
        }
        else
        {
            playerController.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFireball(ref RaycastHit hit)
    {
        return playerController.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location) 
    {
        playerController.GetAnimator().TriggerAttack();

        playerController.transform.LookAt(new Vector3(location.x, playerController.transform.position.y, location.z));

        Vector3 spawnPosition = playerController.transform.position + playerController.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FireballCA>().SetFactionID(playerController.GetFactionID());
        newAttack.GetComponent<FireballCA>().SetShootDirection(playerController.transform.forward);
        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);
        float calculatedDamage = 1 + (2 * skillLevel);
        newAttack.GetComponent<FireballCA>().InitializeDamage(calculatedDamage, playerController.gameObject);
    }
}
