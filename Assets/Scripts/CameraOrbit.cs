using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    [Header("// SENSITIVITY ------------------------------------------------------------------------------------------")]
    [SerializeField] private float mouseSensitivity;
    [Space(5)]
    [SerializeField] private float stickSensitivityX;
    [SerializeField] private float stickSensitivityY;

    [Header("// CALMP ------------------------------------------------------------------------------------------")]
    [SerializeField] private float topClamp;
    [SerializeField] private float bottomClamp;

    private float yaw;
    private float pitch;

    [Header("// SMOOTH HEIGHT ------------------------------------------------------------------------------------------")]
    [SerializeField] private float heightFollowSpeed;
    [SerializeField] private float heightLimit;
    private float smoothY;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        smoothY = target.position.y;
    }

    private void Update()
    {
        // Take mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        // Take stick movement
        float stickX = Input.GetAxis("RightStickX") * stickSensitivityX * Time.deltaTime;
        float stickY = Input.GetAxis("RightStickY") * stickSensitivityY * Time.deltaTime;


        yaw += mouseX + stickX;
        pitch += mouseY + stickY; // It's not inverted because on controller feels better for me:)
        pitch = Mathf.Clamp(pitch, bottomClamp, topClamp);
    }

    private void LateUpdate()
    {
        // Hight Smooth
        float targetY = target.position.y;

        if (Mathf.Abs(targetY - smoothY) > heightLimit) smoothY = Mathf.Lerp(smoothY, targetY, Time.deltaTime * heightFollowSpeed);
        Vector3 focusPoint = new Vector3(target.position.x, smoothY, target.position.z);


        // Rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = focusPoint + rotation * offset;


        // Look At
        Quaternion lookRotation = Quaternion.LookRotation(focusPoint - desiredPosition);
        transform.SetPositionAndRotation(desiredPosition, lookRotation);
    }
}