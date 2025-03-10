using UnityEngine;

public class SpiderQueenEnterTrigger : MonoBehaviour
{
    [SerializeField] BasicAnimator animator;
    [SerializeField] SpiderQueenAI spiderQueen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             spiderQueen.TriggerEntering(animator);
            //if (animator != null)
            //{
            //    animator.SetEntering(true);
            //}
        }
    }

}
