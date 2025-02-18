using UnityEngine;

public class FlameShockwaveCA : CombatActor
{
    private ParticleSystem ps;

    public float lifetime = .5f;
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
        Debug.Log($"{this.name} FlameShockwave OnTriggerEnter " + other.name);
        // Check if the hit object has a CombatReceiver component.
        //CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        if (combatReceiver != null)
        {
            Debug.Log("FlameShockwave collision " + other.name + " faction: " + combatReceiver.GetFactionID());
        }
        // Make sure we don't damage our own faction.
        if (combatReceiver != null && combatReceiver.GetFactionID() != factionID)
        {
            Debug.Log("Flame Shockwave collided with enemy! " + other.name);
            // Call our base logic to apply damage.
            HitReceiver(combatReceiver);

            // Optionally, you might add visual effects or sounds here.
            // For example, if you want to play a small explosion effect:
            EffectsManager.instance.PlayBigBoom(other.transform.position, .5f); 
        }
    }
}
