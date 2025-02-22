using UnityEngine;

public class PlayerAnimator : BasicAnimator
{
    void Update()
    {
        DeltaMovement();
        //deltaPos = transform.position - oldPos;

        //if (deltaPos.sqrMagnitude > .01f * Time.deltaTime)
        //        SetWalking(true);
        //else 
        //    SetWalking(false);

        //oldPos = transform.position;
    }
}
