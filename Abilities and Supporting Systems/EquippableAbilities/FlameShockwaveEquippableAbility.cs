using UnityEngine;

public class FlameShockwaveEquippableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 1;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastFlameShockwave(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            playerController.Movement().MoveToLocation(playerController.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayWhooshExplosionSFX();
            playerController.Combat().SpendMana(manaCost);
        }
        else
        {
            playerController.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFlameShockwave(ref RaycastHit hit)
    {
        return playerController.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        Debug.Log($"FlameShockwave: {this.name} SpawnEquippedAttack");
        playerController.transform.LookAt(new Vector3(location.x, playerController.transform.position.y, location.z));

        Vector3 spawnPosition = location;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FlameShockwaveCA>().SetFactionID(playerController.GetFactionID());
        //newAttack.GetComponent<FlameShockwaveCA>().SetShootDirection(myPlayer.transform.forward);
        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);

        float calculatedDamage = .1f + (.1f * skillLevel);
        //Debug.Log($"Calculated damage: {calculatedDamage}");
        newAttack.GetComponent<FlameShockwaveCA>().InitializeDamage(calculatedDamage, playerController.gameObject);
    }
}
