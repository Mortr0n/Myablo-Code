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
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayElectricLightningSFX();
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastLightning(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {

        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<LightningCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<LightningCA>().SetShootDirection(myPlayer.transform.forward);

        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);

        float calculatedDamage = .25f + (.25f * skillLevel);
        newAttack.GetComponent<LightningCA>().InitializeDamage(calculatedDamage);
    }
}
