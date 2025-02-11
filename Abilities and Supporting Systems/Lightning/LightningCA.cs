using UnityEngine;

public class LightningCA : CombatActor
{

    private ParticleSystem ps;
    private Vector3 shootDirection;

    public float lifetime = .5f;
    //// How long the lightning effect should follow the mouse, in seconds.
    //public float followMouseDuration = 2f;
    //private float followMouseTimer = 0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        // Optionally, you can verify settings or do any initialization here.
        // For example: Debug.Log("Lightning Particle System started with collision messages enabled.");
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Set the initial shoot direction for the lightning and enable mouse following.
    /// </summary>
    /// <param name="newDirection">Initial direction vector.</param>
    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection.normalized;
        transform.forward = shootDirection;
        //followMouseTimer = followMouseDuration;
    }

    void Update()
    {
        //// If the timer is active, update the shoot direction to follow the mouse.
        //if (followMouseTimer > 0)
        //{
        //    followMouseTimer -= Time.deltaTime;

        //    // Define a horizontal plane at the lightning's Y position.
        //    Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        //    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    float enter;
        //    if (plane.Raycast(mouseRay, out enter))
        //    {
        //        Vector3 mouseWorldPos = mouseRay.GetPoint(enter);
        //        // Calculate the new direction from the lightning's position to the mouse world position.
        //        shootDirection = (mouseWorldPos - transform.position).normalized;
        //        transform.forward = shootDirection;
        //    }
        //}
    }

    //TODO: need to figure out why lighning doesn't hit the spawners.  I think it's probably a layer thing!

    /// <summary>
    /// This method is called by Unity when particles from this ParticleSystem
    /// collide with another GameObject's collider.
    /// </summary>
    /// <param name="other">The GameObject that was hit by one or more particles.</param>
    void OnChildParticleCollision(GameObject other)
    {
       
        // Check if the hit object has a CombatReceiver component.
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        Debug.Log("Lighning collision " + other.name + "faction: " + combatReceiver.GetFactionID());

        // Make sure we don't damage our own faction.
        if (combatReceiver != null && combatReceiver.GetFactionID() != factionID)
        {
            Debug.Log("Lightning collided with enemy! " + other.name);
            // Call our base logic to apply damage.
            HitReceiver(combatReceiver);

            // Optionally, you might add visual effects or sounds here.
            // For example, if you want to play a small explosion effect:
            EffectsManager.instance.PlayLightningHit(other.transform.position, .5f);
        }
    }
}
