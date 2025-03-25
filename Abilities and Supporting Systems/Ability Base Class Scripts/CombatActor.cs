using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected int factionID = 0;
    protected GameObject owner;
    protected float damage = 1;

    public virtual void InitializeDamage(float amount, GameObject ownerRef)
    {
        damage = amount;
        owner = ownerRef;
    }

    public void SetFactionID(int newID)
    {
        factionID = newID;
    }

    protected virtual void HitReceiver(CombatReceiver target)
    {
        Debug.Log($"inside hit receiver");
        EnemyAI enemyAI = target.GetComponent<EnemyAI>();


        if (enemyAI != null)
        {
            Debug.Log($"Enemy AI: {enemyAI}");
            Debug.Log($"Enemy AI Triggering Attacking on target {owner.name}");
            enemyAI.TriggerAttacking(owner);
        }
        Debug.Log($"Receiver {target.name} is taking damage from: {owner.name} for damage: {damage}");
        target.TakeDamage(damage);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        Debug.Log($"{this.name} on trigger combat actor oTrigger: {other.isTrigger}");
        if (combatReceiver == null)
        {
            Debug.Log($"Null Receiver {this.name}: isName ::: combat receiver {combatReceiver} is trigger? {other.isTrigger} faction: {factionID} {this.name}");
            return;
        }
        if (combatReceiver != null)
        {
            Debug.Log($"{this.name}: isName ::: combat receiver {combatReceiver} is trigger? {other.isTrigger} faction: {combatReceiver.GetFactionID()} otherfaction: {factionID} {this.name}");
        }

        Debug.Log($"{combatReceiver.name} is trigger? {other.isTrigger} t/f: {combatReceiver != null && !other.isTrigger} or {this.name} is Damaging ThingName: {other.name} for damage: {damage}");
        if (!other.isTrigger)
        {
            Debug.Log($"Inside first if trigger? {other.isTrigger}");
            if (combatReceiver.GetFactionID() != factionID)
            {
                Debug.Log($"Inside second if trigger? {combatReceiver}");
                HitReceiver(combatReceiver);
            }
        } 
    } 
}
