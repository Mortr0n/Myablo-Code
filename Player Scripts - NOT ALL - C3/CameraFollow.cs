using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target (player) that the camera will follow
    public Transform target;

    [Header("Camera Distances")]
    [SerializeField] public float distance = 7f; // The distance behind the player
    [SerializeField] public float height = 14f; // The height above the player

    [Header("Camera Rotation")]
    [SerializeField] public float rotationSpeed = 100f; // How quickly we rotate around the target when pressing horizontal keys
    [SerializeField] public float pitchAngle = -22;

    // We’ll keep track of our current rotation around the Y-axis
    private float currentYRotation = 0f;

    void LateUpdate()
    {
        // Safety check
        if (!target)
        {
            return;
        }


        // 1. Get horizontal input (arrow keys, A/D, or joystick X axis by default)
        //float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.Q)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.E)) horizontalInput = 1f;

        // 2. Update our rotation value based on horizontal input
        currentYRotation += horizontalInput * rotationSpeed * Time.deltaTime;

        // 3. Create a rotation that includes pitch (X) and currentYRotation (Y)
        Quaternion rotation = Quaternion.Euler(pitchAngle, currentYRotation, 0f);

        // 4. Calculate the desired position (offset from the target)
        Vector3 offset = new Vector3(0f, height, -distance);
        Vector3 desiredPosition = target.position + rotation * offset;

        // 5. Move the camera to this position
        transform.position = desiredPosition;

        // 6. Make sure the camera is looking at the target
        transform.LookAt(target.position);
    }
}
