using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    public float movementAmount = 0.1f; // bude lepší to přiřadit k holderu, jeluikož to tady kolikudje s recoil scriptem
    public float smoothing = 3f; // Adjust this value to control the smoothness of the gun movement

    public float tiltAmount = 10f; // Adjust this value to control the amount of gun tilting
    public float smoothingRotation = 5f; // Adjust this value to control the smoothness of the gun movement
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public Rigidbody playerRigidbody;
    public LayerMask whatIsGround;
    private bool wasGrounded = true;
    public float playerHeight;

    void Start()
    {
        initialPosition = transform.localPosition;

        initialRotation = transform.localRotation;
    }

    void Update()
    {
        MoveGunWithPlayer();
    }

    void MoveGunWithPlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the target position based on player input
        Vector3 targetPosition = initialPosition + new Vector3(horizontalInput, 0f, verticalInput) * movementAmount;

        // Smoothly interpolate to the target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothing * Time.deltaTime);

        // Calculate the target rotation based on player input
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, horizontalInput * tiltAmount, 0f);

        // Smoothly interpolate to the target rotation


        // Tilt gun based on player's vertical velocity
        float verticalVelocity = playerRigidbody.velocity.y * 20;
        float tiltAngle = Mathf.Clamp(verticalVelocity * 0.1f, -tiltAmount, tiltAmount);
        targetRotation *= Quaternion.Euler(tiltAngle, 0f, 0f);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothingRotation * Time.deltaTime);

        // Check if player just landed on the ground
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (!wasGrounded && isGrounded)
        {
            // Player just hit the ground, adjust the gun rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, smoothing * Time.deltaTime);
        }

        wasGrounded = isGrounded;

    }
}
