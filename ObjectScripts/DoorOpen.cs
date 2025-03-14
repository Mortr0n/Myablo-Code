using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Animator gateAnimator;
    
    private bool doorOpen = false;
    [SerializeField] private bool _isOpenable = false;
    public bool IsOpenable { get { return _isOpenable; } set { _isOpenable = value; } }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isOpenable) // currently set via collectible
            {
                gateAnimator.SetBool("isDoorOpen", true);
                doorOpen = true;
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
