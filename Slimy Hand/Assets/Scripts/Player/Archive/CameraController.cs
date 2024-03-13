using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 2.5f;
    public Transform camTarget;

    [HideInInspector] public float x, y;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        PlayerInput();
        RotateCamera();
    }
    void PlayerInput()
    {
        y += Input.GetAxisRaw("Mouse X") * lookSpeed;
        x += Input.GetAxisRaw("Mouse Y") * lookSpeed;

        x = Mathf.Clamp(x, -90, 90);
    }
    void RotateCamera()
    {
        transform.position = camTarget.position;

        transform.rotation = Quaternion.Euler(-x, y, 0);
        PlayerStateMachine.context.transform.rotation = Quaternion.Euler(0, y, 0);
    }
}