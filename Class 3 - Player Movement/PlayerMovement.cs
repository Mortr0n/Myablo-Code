using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject testSphere; //this was for testing.  No longer needed, but useful for seeing if your raycast stuff works remember to add the sphere to the player prefab field
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        // Original way before MoveToLocation was made.
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RunClickMovement();
        //}
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

}
