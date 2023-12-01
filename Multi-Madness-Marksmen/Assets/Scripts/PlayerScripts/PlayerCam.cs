using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update

    public float sensX;
    public float sensY;
    public bool isEnabled = true;

    public Transform orientation;

    public GameObject cameraRotation;

    public float xRotation;
    public float yRotation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;    
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraRotation.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
