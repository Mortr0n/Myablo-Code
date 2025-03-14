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
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);

            AudioManager.instance.PlayFireballSFX();
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFireball(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location) 
    {
        myPlayer.GetAnimator().TriggerAttack();

        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FireballCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<FireballCA>().SetShootDirection(myPlayer.transform.forward);
        int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);
        float calculatedDamage = 1 + (2 * skillLevel);
        newAttack.GetComponent<FireballCA>().InitializeDamage(calculatedDamage);
    }
}
