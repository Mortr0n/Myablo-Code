using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject testSphere; //this was for testing.  No longer needed, but useful for seeing if your raycast stuff works remember to add the sphere to the player prefab field
    NavMeshAgent agent;

    [SerializeField] DashCooldownUI dashCooldownUI;
    bool isDashing = false;
    float dashDuration = .3f;
    [SerializeField] float dashSpeed = 1045f;
    float dashPauseTime = 1f;
    private float rotationSpeed = 10f;



    Vector3 lastMovementDirection = Vector3.zero;
    Vector3 dashEnd = Vector3.zero;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        dashCooldownUI = FindFirstObjectByType<DashCooldownUI>();

    }


    void Update()
    {
        if (agent.velocity.magnitude > .1f)
        {
            lastMovementDirection = agent.velocity.normalized;
        }
        KeyboardMovement();
    }

    void KeyboardMovement()
    {
        //var vertical = Input.GetAxis("Vertical");
        //var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        if (vertical != 0 || horizontal != 0)
        {
            if (agent.hasPath) agent.ResetPath();
        }

        // Get camera forward and right vectors (ignoring vertical tilt)
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // Ignore up/down tilt
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0; // Ignore up/down tilt
        cameraRight.Normalize();

        // Create movement direction based on camera's forward/right
        Vector3 moveDirection = (cameraForward * vertical) + (cameraRight * horizontal);
        //moveDirection = moveDirection.normalized * agent.speed * Time.deltaTime;

        // Move only if there's input
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            moveDirection.Normalize();
            agent.Move(moveDirection * agent.speed * Time.deltaTime );

            // Rotate character to face movement direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveDirection),
                10f * Time.deltaTime // Smooth rotation speed
            );  
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
            //dashCooldownUI = GetComponent<DashCooldownUI>();
            dashCooldownUI.StartDashCooldown(dashPauseTime);
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

        //// Calculate dash target position
        //Vector3 dashStart = transform.position;
        //Vector3 dashTarget = dashStart + direction.normalized * dashSpeed * dashDuration;

        float elapsedTime = 0;
        while (elapsedTime < dashDuration)
        {
            agent.Move(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // re-enable the navmesh movement reset speed and set dash to false
        agent.isStopped = false;
        agent.speed = originalSpeed;
        //isDashing = false;
        StartCoroutine(EnableDashTimer(dashPauseTime));

        // need to stop character from trying to run to original destination after dash
        agent.ResetPath();
    }

    IEnumerator EnableDashTimer(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        isDashing = false;

    }

}
