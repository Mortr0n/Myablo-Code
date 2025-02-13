using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject testSphere; //this was for testing.  No longer needed, but useful for seeing if your raycast stuff works remember to add the sphere to the player prefab field
    NavMeshAgent agent;
    bool isDashing = false;
    [SerializeField] float dashDuration = .2f;
    [SerializeField] float dashSpeed = 10f;
    Vector3 lastMovementDirection = Vector3.zero;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (agent.velocity.magnitude > .1f)
        {
            lastMovementDirection = agent.velocity.normalized;
        }
    }


    void RunClickMovement()
    {
        Debug.Log("Test Run Click");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.point != Vector3.zero) // zero value means something went wrong getting the hit.point because it will return (0, 0, 0)
            {
                //testSphere.transform.position = hit.point; // originally to move the sphere after the clicker before we added in the navmesh agent
                agent.destination = hit.point; // nav mesh agent on player prefab.  Go to this position of the raycast we labeled hit and then .point to go to that hit point
            }
        }

    }

    public void MoveToLocation(Vector3 location)
    {
        agent.destination = location;
    }
    public void PerformDash(Vector3 direction)
    {
        Debug.Log($"Perform Dash {direction} isDashing? {isDashing}");
        if (!isDashing)
        {
            StartCoroutine(DashCoroutine(direction));
        }
    }

    public Vector3 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }

    IEnumerator DashCoroutine(Vector3 direction)
    {
        isDashing = true;
        float originalSpeed = agent.speed;
        agent.isStopped = true;

        float elapsedTime = 0;
        while (elapsedTime < dashDuration)
        {
            agent.Move(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false;
        agent.speed = originalSpeed;
        isDashing = false;
    }

}
