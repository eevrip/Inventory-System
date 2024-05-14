using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 80f;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Mouse Cursor Doesn't appear
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity; //Very slow movement if mouseSensitivity = 1f
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

       
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Only values between these constants

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Rotates the camera in x axis
        playerBody.Rotate(Vector3.up * mouseX); //Rotate the body of player in y axis
    }
}
