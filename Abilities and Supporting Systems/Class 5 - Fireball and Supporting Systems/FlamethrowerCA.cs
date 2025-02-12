using UnityEngine;

public class FlamethrowerCA : CombatActor
{
    private ParticleSystem ps;
    private Vector3 shootDirection;

    public float lifetime = .5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, lifetime);
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection.normalized;
        transform.forward = shootDirection;
        //followMouseTimer = followMouseDuration;
    }

    void OnChildParticleCollision(GameObject other)
    {

        // Check if the hit object has a CombatReceiver component.
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        Debug.Log("flamethrower collision " + other.name + "faction: " + combatReceiver.GetFactionID());

        // Make sure we don't damage our own faction.
        if (combatReceiver != null && combatReceiver.GetFactionID() != factionID)
        {
            Debug.Log("Flamethrower collided with enemy! " + other.name);
            // Call our base logic to apply damage.
            HitReceiver(combatReceiver);

            // Optionally, you might add visual effects or sounds here.
            // For example, if you want to play a small explosion effect:
            EffectsManager.instance.PlayBigBoom(other.transform.position, .5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
