using UnityEngine;

public class PlayerModelFollow : MonoBehaviour
{
    public Transform cameraTransform; // Assign Main Camera
    public float followSpeed = 10f;   // Adjust for smoothness

    void Update()
    {
        // Instead of teleporting, smoothly follow the camera
        Vector3 targetPosition = cameraTransform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotate model smoothly with camera
        Quaternion targetRotation = cameraTransform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}
