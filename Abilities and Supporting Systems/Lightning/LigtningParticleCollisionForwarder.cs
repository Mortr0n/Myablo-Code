using UnityEngine;

public class LigtningParticleCollisionForwarder : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Lightning collision: {other.name}");
        // This will call OnChildParticleCollision on this object and all parents.
        SendMessageUpwards("OnChildParticleCollision", other, SendMessageOptions.DontRequireReceiver);
    }
}
