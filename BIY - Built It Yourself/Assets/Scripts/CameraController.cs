using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public Vector2 panLimit;
    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;

    public float rotationSpeed = 5.0f; 
    private float yaw = 0.0f; 
    private float pitch = 0.0f;

    void Update()
    {
      
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0; 
        right.y = 0; 

        forward.Normalize();
        right.Normalize();

        if (Input.GetKey("w"))
        {
            transform.position += forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            transform.position += right * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= right * panSpeed * Time.deltaTime;
        }

        
       
        if (Input.GetMouseButton(1)) 
        {
            yaw += rotationSpeed * Input.GetAxis("Mouse X");
            pitch -= rotationSpeed * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -35f, 60f); 
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
