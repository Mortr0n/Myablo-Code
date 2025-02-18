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
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            //AudioManager.instance.PlaySceneSwitchSwooshSFX();
            AudioManager.instance.PlaySceneSwitchSwooshSFX();
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }

    private bool CanCastFlameShockwave(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        Debug.Log($"FlameShockwave: {this.name} SpawnEquippedAttack");
        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = location;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FlameShockwaveCA>().SetFactionID(myPlayer.GetFactionID());
        //newAttack.GetComponent<FlameShockwaveCA>().SetShootDirection(myPlayer.transform.forward);

        float calculatedDamage = .1f + (.1f * skillLevel);
        Debug.Log($"Calculated damage: {calculatedDamage}");
        newAttack.GetComponent<FlameShockwaveCA>().InitializeDamage(calculatedDamage);
    }
}
