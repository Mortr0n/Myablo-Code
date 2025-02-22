using UnityEngine;

public class HealSelfCA : CombatActor
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    
}
