using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float Movement_Speed = 5f;
    public float Jump_Strenght = 5f;//Síla skoku    
    //Mouse Input
        public float sensitivity = 6f;
        float rotationX = 0f;
        float rotationY = 0f;
    // Start is called before the first frame update

    bool IsGrounded()// Je hráč na zemi? Výstup True, False
    {
        return GetComponent<Rigidbody>().velocity.y == 0;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Movement

        rotationY += Input.GetAxis("Mouse Y")* -1 *sensitivity;
        rotationX += Input.GetAxis("Mouse X")* sensitivity;

        transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

        //Key Input/Movement
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Jump_Strenght, ForceMode.Impulse);
        }

        float Horizontal_Input = Input.GetAxis("Horizontal");
        float Vertical_Input = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(Horizontal_Input, 0f, Vertical_Input) * Movement_Speed * Time.deltaTime;

        transform.Translate(Movement);
    }
}
