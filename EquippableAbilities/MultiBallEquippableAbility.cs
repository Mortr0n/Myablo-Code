using UnityEngine;

public class MultiBallEquippableAbility : FireballEquipableAbility
{
    protected override void SpawnEquippedAttack(Vector3 location)
    {
        base.SpawnEquippedAttack(location);
        base.SpawnEquippedAttack(location + transform.right);
        base.SpawnEquippedAttack(location - transform.right);
    }
}
