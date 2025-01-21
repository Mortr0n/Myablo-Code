using UnityEngine;

public class EnemyAnimator : BasicAnimator
{
    protected virtual void Update()
    {
        DeltaMovement();
    }
    // this below should actually be made to be in the basic animator since we're using in playeranimator and enemyanimator 
    // made it into DeltaMovement for importing to the variouis animators as needed
    //void Update()
    //{
    //    deltaPos = transform.position - oldPos;

    //    if (deltaPos.sqrMagnitude > .01f * Time.deltaTime)
    //        SetWalking(true);
    //    else
    //        SetWalking(false);

    //    oldPos = transform.position;
    //}
}
