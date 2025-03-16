using UnityEngine;

public class LockToParent : MonoBehaviour
{
    private Vector3 localOffset;
    private Transform parent;

    void Start()
    {
        parent = transform.parent; // The root object
        localOffset = transform.localPosition; // Store initial offset
    }

    void LateUpdate()
    {
        // Keep the model locked to the parent position
        transform.position = parent.position + parent.TransformDirection(localOffset);
    }
}
