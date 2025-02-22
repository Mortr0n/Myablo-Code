using UnityEngine;

public class TornadoParticleCollisionForwarder : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Tornado collision: {other.name}");
        // This will call OnChildParticleCollision on this object and all parents.
        SendMessageUpwards("OnChildParticleCollision", other, SendMessageOptions.DontRequireReceiver);
    }
}
