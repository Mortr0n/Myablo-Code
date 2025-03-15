using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected int factionID = 0;
    protected float damage = 1;

    public virtual void InitializeDamage(float amount)
    {
        damage = amount;
    }

    public void SetFactionID(int newID)
    {
        //Debug.Log("Set Faction ID");
        factionID = newID;
    }

    protected virtual void HitReceiver(CombatReceiver target)
    {
        //Debug.Log($"Damaging {target}");
        EnemyAI enemyAI = target.GetComponent<EnemyAI>();
        // TODO:  THIS I think is how it's done!
        if (enemyAI != null)
        {
            enemyAI.TriggerPursuing(this.gameObject);
        }
        target.TakeDamage(damage);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"on trigger combat actor ");
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        if (combatReceiver != null)
        {
            Debug.Log($"{this.name}: isName ::: combat receiver {combatReceiver} is trigger? {other.isTrigger} faction: {combatReceiver.GetFactionID()} otherfaction: {factionID} {other.name}");
        }
        if (combatReceiver != null && !other.isTrigger)
        {
            //Debug.Log($"Inside first if trigger? {other.isTrigger}");
            if (combatReceiver.GetFactionID() != factionID)
            {
                HitReceiver(combatReceiver);
            }
        }
    }
}
