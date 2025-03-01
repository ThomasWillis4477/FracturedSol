using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    public Transform cameraRoot; // This should be PlayerCameraRoot
    public Transform playerBody;

    private float xRotation = 0f;
    private bool cursorLocked = true;

    void Start()
    {
        LockCursor(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            LockCursor(!cursorLocked);
        }

        if (!cursorLocked) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate up/down (camera only)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate left/right (player model)
        playerBody.Rotate(Vector3.up * mouseX);

        // **Manually sync the camera to PlayerCameraRoot**
        transform.position = cameraRoot.position;
    }

    void LockCursor(bool isLocked)
    {
        cursorLocked = isLocked;
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}
