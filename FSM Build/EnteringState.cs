using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnteringState : EnemyStateBase
{
    protected GameObject stateTarget;
    protected UnityEngine.AI.NavMeshAgent agent;


    public EnteringState(BasicAnimator animator) 
    {
        //this.stateTarget = target;
    }

    public void Enter(EnemyAI ai)
    {
        agent = ai.GetComponent<UnityEngine.AI.NavMeshAgent>();

        base.Enter(ai);

        //// Set up the wake-up animation
        //BasicAnimator animator = ai.GetComponent<BasicAnimator>();
        //Debug.Log($"target: {stateTarget}");
        //if (animator != null)
        //{
        //    Debug.Log($"target in anim {animator} if {stateTarget.name}");
        //    animator.SetEntering(true);
        //    ai.monoBehaviour.StartCoroutine(EnteranceWait(ai, animator, stateTarget));
        //}
    }

    //public virtual IEnumerator EnteranceWait(EnemyAI ai, BasicAnimator animator, GameObject targetToPursue)
    //{
    //    Debug.Log($"ai: {ai}, anim: {animator}, target: {targetToPursue}");
    //    yield return new WaitForSeconds(2.1f);
    //    animator.SetEntering(false);
    //    ai.TriggerPursuing(targetToPursue);
    //}

    public void Exit() 
    {
        
    }

    public void Update()
    {
       
    }

}
