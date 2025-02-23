using UnityEngine;

public class HealSelfCA : CombatActor
{
    private void Update()
    {
        if (PlayerController.instance.Combat().GetCurrentShield() <= 0) Destroy(gameObject, .1f);
    }
    
}
