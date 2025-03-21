using UnityEngine;

public class TornadoEquippableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 1;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastTornado(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            //TODO: Removed because I don't think I want the player to move.  Check in flame shocwave also if this works 
            //myPlayer.Movement().MoveToLocation(myPlayer.transform.position); 
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayWindStormSFX();
            playerController.Combat().SpendMana(manaCost);
        }
        else
        {
            playerController.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastTornado(ref RaycastHit hit)
    {
        return playerController.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {

        playerController.transform.LookAt(new Vector3(location.x, playerController.transform.position.y, location.z));

        Vector3 spawnPosition = location;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);

        newAttack.GetComponent<TornadoAttackCA>().SetFactionID(playerController.GetFactionID());
        //newAttack.GetComponent<FlameShockwaveCA>().SetShootDirection(myPlayer.transform.forward);
        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);

        float calculatedDamage = .15f + (.15f * skillLevel);
        //Debug.Log($"Calculated damage: {calculatedDamage}"); 
        newAttack.GetComponent<TornadoAttackCA>().InitializeDamage(calculatedDamage, playerController.gameObject);
    }
}
