using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] bool hasKey;


    public bool HasKey { set { hasKey = value; } get { return hasKey; } }
}
