using UnityEngine;

public class RoomDoorOpen : MonoBehaviour
{
    [SerializeField] private Animator gateAnimator;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private Room parentRoom;

    private bool doorOpen = false;
    //private bool _isOpenable = false;
    //public bool IsOpenable { get { return _isOpenable; } set { _isOpenable = value; } }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (roomManager.HasKeyForRoom(parentRoom))
            {
                gateAnimator.SetBool("isDoorOpen", true);
                doorOpen = true;
            }
            else
            {
                Debug.Log($"Player doesn't have key for the room {parentRoom.name}");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && doorOpen)
        {

            gateAnimator.SetBool("isDoorOpen", false);
            doorOpen = false;
        }
    }
}
