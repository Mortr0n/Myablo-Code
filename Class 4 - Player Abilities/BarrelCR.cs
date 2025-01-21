using UnityEngine;

public class BarrelCR : CombatReceiver
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
