using UnityEngine;

public class MeleeAttackCA : CombatActor
{
    void Start()
    {
        Destroy(gameObject, .1f);
    }

}
