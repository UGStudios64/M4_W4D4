using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3 (0, 0, 0);
    [SerializeField] float mouseSensivity;
    [SerializeField] float bottomClamp;
    [SerializeField] float topClamp;

    float yaw;
    float pitch;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Take mouse movement
        yaw += Input.GetAxis("Mouse X") * mouseSensivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensivity;
        pitch = Mathf.Clamp(pitch, bottomClamp, topClamp);
    }

    private void LateUpdate()
    {
        // Rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Look At
        Vector3 lookAt = target.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookAt - desiredPosition);
        transform.SetPositionAndRotation(desiredPosition, lookRotation);
    }
}