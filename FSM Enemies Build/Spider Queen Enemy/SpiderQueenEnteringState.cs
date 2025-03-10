using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderQueenEnteringState : EnemyStateBase
{
    //[SerializeField] protected GameObject stateTarget;
    [SerializeField] protected BasicAnimator animator;


    public SpiderQueenEnteringState(BasicAnimator animator)
    {
        this.animator = animator;
        //this.stateTarget = target;
        //animator = GetComponentInParent<BasicAnimator>();
    }

    public override void Enter(EnemyAI ai)
    {
        base.Enter(ai);
        animator.SetEntering(true);
        ai.SetSleeping(false);
        Debug.Log($"sleep: {ai.GetSleeping()}, Enter: {animator.GetEntering()}");
    }

    public override void Update(EnemyAI ai)
    {
        base.Update(ai);      
    }

    //public IEnumerator EnteranceWait(EnemyAI ai, BasicAnimator animator, GameObject targetToPursue) 
    //{
    //    Debug.Log($"ai: {ai}, anim: {animator}, target: {targetToPursue}");
    //    yield return new WaitForSeconds(2.1f);
    //    animator.SetEntering(false);
    //    //ai.TriggerPursuing(targetToPursue);
    //    ai.TriggerWandering();
    //}


    public override void Exit(EnemyAI ai)
    {
        base.Exit(ai);

        // Clean up animation state
        BasicAnimator animator = ai.GetComponent<BasicAnimator>();
        if (animator != null)
        {
            animator.SetEntering(false);
        }
    }

   
}
