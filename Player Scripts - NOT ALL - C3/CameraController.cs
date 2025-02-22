using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.XR;

public class CameraController : MonoBehaviour
{
    GameObject followTarget;
    // TODO: Set follow target needs reenabled in the player controller under Start() if I try to use the camera controller again.
    // Update is called once per frame
    //void Update()
    //{
    //    if (followTarget != null) Follow();


    //}

    //public void SetFollowTarget(GameObject target)
    //{
    //    followTarget = target;
    //}

    //void Follow()
    //{
    //    transform.position = followTarget.transform.position + offsetVector;
    //    transform.LookAt(followTarget.transform.position);
    //}
}
