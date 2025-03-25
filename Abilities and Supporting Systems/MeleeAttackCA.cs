using UnityEngine;

public class MeleeAttackCA : CombatActor
{
    void Start()
    {
        Destroy(gameObject, .1f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //Debug.Log("on trigger combat actor Melee CA"); 
    }

}
