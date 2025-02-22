using UnityEngine;

public class EnemyCombat : CombatReceiver
{
    public override void Die()
    {
        //Debug.Log("Died!  EnemyCombat");
        base.Die();
        // notify the AI when the combat receiver dies

        GetComponent<EnemyAI>().TriggerDeath();
        // grant the player experience
    }
}
