using UnityEngine;

public class FlamethrowerEquipableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 1;

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastFlamethower(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayBigFlameSFX();
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFlamethower(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {

        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FlamethrowerCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<FlamethrowerCA>().SetShootDirection(myPlayer.transform.forward);

        float calculatedDamage = .25f + (.25f * skillLevel);
        Debug.Log($"Calculated damage: {calculatedDamage}");
        newAttack.GetComponent<FlamethrowerCA>().InitializeDamage(calculatedDamage);
    }
}
