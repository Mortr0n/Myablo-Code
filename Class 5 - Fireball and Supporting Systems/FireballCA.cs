using UnityEngine;

public class FireballCA : CombatActor
{
    [SerializeField] float speed = 25f;
    Vector3 shootDirection = Vector3.zero;


    void Start()
    {
        Destroy(gameObject, 5f);
    }


    void FixedUpdate()
    {
        transform.Translate(shootDirection * speed * Time.fixedDeltaTime);
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection;
    }

    protected override void HitReceiver(CombatReceiver target)
    {
        base.HitReceiver(target);
        EffectsManager.instance.PlaySmallBoom(transform.position, 1);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        //Debug.Log("on trigger combat actor ");
        CombatReceiver combatReceiver = other.GetComponent<CombatReceiver>();
        //Debug.Log($"combat receiver {combatReceiver} is trigger? {other.isTrigger}");
        if (combatReceiver != null && !other.isTrigger)
        {
            //Debug.Log($"Inside first if trigger? {other.isTrigger}");
            if (combatReceiver.GetFactionID() != factionID)
            {
                HitReceiver(combatReceiver);
                Destroy(gameObject);
            }
        }
    }
}
