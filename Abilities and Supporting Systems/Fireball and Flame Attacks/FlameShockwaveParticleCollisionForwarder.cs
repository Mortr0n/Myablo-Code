using UnityEngine;

public class FlameShockwaveParticleCollisionForwarder : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"FlameShockwave collision: {other.name}");
        // This will call OnChildParticleCollision on this object and all parents.
        SendMessageUpwards("OnChildParticleCollision", other, SendMessageOptions.DontRequireReceiver);
    }
}
