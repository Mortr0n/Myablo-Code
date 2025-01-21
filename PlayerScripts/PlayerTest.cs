using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private Rigidbody playerRb;

    private float speed = 10f;
    private float turnSpeed = 3f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (playerRb != null)
        {
            Move();
            Turn();
        }
    }

    void Move()
    {
        float xDir = Input.GetAxis("Vertical"); 

        //Vector3 direction = new Vector3 (xDir, 0, 0);
        Vector3 direction = transform.forward * xDir;
        Debug.DrawLine(transform.position, transform.position + transform.right *2, Color.green);
        playerRb.AddForce(direction * speed, ForceMode.Force);

    }

    void Turn()
    {
        float zDir = Input.GetAxis("Horizontal");

        float turnAmount = zDir * turnSpeed;
        Quaternion rotationZ = Quaternion.Euler(0, turnAmount, 0);

        playerRb.MoveRotation(playerRb.rotation * rotationZ);
    }
}
