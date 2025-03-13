using UnityEngine;

public class FireballEquipableAbility : EquippableAbility
{
    [SerializeField] float manaCost = 5;
    //public override void RunAbilityClicked(PlayerController player)
    //{
    //    myPlayer = player;
    //    targetedReceiver = null;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    Physics.queriesHitTriggers = false;

    //    if(Physics.Raycast(ray, out hit))
    //    {
    //       SuccessfulRaycastFunctionality(ref hit);
    //    }
    //}

    protected override void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        //base.SuccessfulRaycastFunctionality(ref hit);
        if (CanCastFireball(ref hit))
        {
            //if(hit.collider.isTrigger) { return; } // don't attack trigger only colliders
            SpawnEquippedAttack(hit.point);
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlayDoubleBoomExplosionSFX();
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

        float calculatedDamage = 1 + (2 * skillLevel);
        newAttack.GetComponent<FireballCA>().InitializeDamage(calculatedDamage);
    }
}
