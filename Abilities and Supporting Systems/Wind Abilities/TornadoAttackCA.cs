using UnityEngine;

public class TornadoAttackCA : CombatActor
{
    private ParticleSystem ps;

    public float lifetime = 1f;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        // Optionally, you can verify settings or do any initialization here.
        // For example: Debug.Log("Lightning Particle System started with collision messages enabled.");
        Destroy(gameObject, lifetime);
    }


    /// <summary>
    /// This method is called by Unity when particles from this ParticleSystem
    /// collide with another GameObject's collider.
    /// </summary>
    /// 
    protected void OnChildParticleCollision(GameObject other)
    {
        Debug.Log($"{this.name} Tornado Attack OnTriggerEnter " + other.name);
        // Check if the hit object has a CombatReceiver component.
        //CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        if (combatReceiver != null)
        {
            Debug.Log("Tornado Attack collision " + other.name + " faction: " + combatReceiver.GetFactionID());
        }
        // Make sure we don't damage our own faction.
        if (combatReceiver != null && combatReceiver.GetFactionID() != factionID)
        {
            Debug.Log("Tornado attack collided with enemy! " + other.name);
            // Call our base logic to apply damage.
            HitReceiver(combatReceiver);

            // Optionally, you might add visual effects or sounds here.
            // For example, if you want to play a small explosion effect:
            EffectsManager.instance.PlayImpactHit(other.transform.position, .5f);
        }
    }
}
