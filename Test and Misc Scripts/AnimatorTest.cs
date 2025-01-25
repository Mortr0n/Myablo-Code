using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void WalkTrigger()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("TestWalk");
        }
    }

    void Jump()
    {
        // Simulate changing the Blend parameter using keys
        if (Input.GetKey(KeyCode.Alpha1)) // Press 1 for Jump_Start
        {
            animator.SetFloat("Blend", 0);
        }
        else if (Input.GetKey(KeyCode.Alpha2)) // Press 2 for Fall
        {
            animator.SetFloat("Blend", 0.5f);
        }
        else if (Input.GetKey(KeyCode.Alpha3)) // Press 3 for Jump_End
        {
            animator.SetFloat("Blend", 1f);
        }
    }
}
