using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;

    [SerializeField] GameObject smallEffect;
    [SerializeField] GameObject bigEffect; 
    [SerializeField] GameObject lightningEffect;
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    void SpawnEffect(GameObject effectPrefab, Vector3 location, float duration, Transform effectParent = null)
    {
        Debug.Log("this script is attached to: " + gameObject.name);
        string path = GetHierarchyPath(transform);
        Debug.Log("Hierarchy path: " +  path);

        GameObject newEffect = Instantiate(effectPrefab, location, Quaternion.identity);
        if (effectParent != null) newEffect.transform.SetParent(effectParent);
        if (duration > 0) Destroy(newEffect, duration);
    }

    string GetHierarchyPath(Transform current)
    {
        if (current.parent == null)
        {
            // If there's no parent, this is the root.
            return current.name;
        }
        else
        {
            // Recursively build the path from the root to this transform.
            return GetHierarchyPath(current.parent) + "/" + current.name;
        }
    }

    public void PlaySmallBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(smallEffect, location, duration, effectParent);
    }
    public void PlayBigBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(bigEffect, location, duration, effectParent);
    }
    public void PlayLightningHit(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(lightningEffect, location, duration, effectParent);
    }
}
