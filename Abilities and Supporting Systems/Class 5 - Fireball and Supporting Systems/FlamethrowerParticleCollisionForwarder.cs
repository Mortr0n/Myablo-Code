using UnityEngine;

public class FlamethrowerParticleCollisionForwarder : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Flamethrower collision: {other.name}");
        // This will call OnChildParticleCollision on this object and all parents.
        SendMessageUpwards("OnChildParticleCollision", other, SendMessageOptions.DontRequireReceiver);
    }
}
