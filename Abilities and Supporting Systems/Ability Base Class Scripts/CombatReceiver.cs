using UnityEngine;

public class CombatReceiver : Clickable
{
    [SerializeField] protected int factionID = 0; //splitting combat system into factions to allow this to be used for all

    [SerializeField] protected float maxHP = 35;
    [SerializeField] protected float currentHP;
    [SerializeField] protected bool alive = true;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public float GetMaxHealth() { return maxHP; }

    public virtual void Die() 
    {
        if (!alive) { return; }
        alive = false;
    }

    public void SetFactionID(int newID)
    {
        factionID = newID;
    } 

    public int GetFactionID() 
    {
        return factionID; 
    }


    public virtual void TakeDamage(float amount)
    {
        Debug.Log($"[TakeDamage()] {this.name} currentHP: {currentHP} Taking Damage {amount}");
        if (!alive) { return; }
        currentHP -= amount;
        if(currentHP <= 0) Die();
    }

    public virtual void Revive()
    {
        
    }


}
