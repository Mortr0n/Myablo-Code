using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    private GameObject player;
    private GameObject gateTrigger;

    void Start()
    {
        //player = GameObject.Find("Player");
        gateTrigger = GameObject.Find("StartingGateTrigger");
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.name}");
        if (other.CompareTag("Player"))
        {
            DoorOpen doorOpen = gateTrigger.GetComponent<DoorOpen>();

            doorOpen.IsOpenable = true;
            Destroy(gameObject);
        }
    }
}
