using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class SimpleMovement : MonoBehaviour
    {
        // Movement Speed variables
        [Header("Movement Speeds")]
        public float walkSpeed = 3.0f;        // Speed of walking
        public float runMultiplier = 2.0f;    // Speed multiplier when running

        // Mouse Sensitivity variables
        [Header("Mouse Sensitivity")]
        public float mouseSensitivity = 2.0f; // Sensitivity of mouse movement
        public float yClamp = 80.0f;          // Vertical rotation clamp

        // Private variables
        private CharacterController controller; // Unity component for character control
        private float verticalRotation;         // Vertical rotation angle
        private Camera mainCamera;              // Main camera in the scene

        // Called when the script is first initialized
        void Start()
        {
            // Get the CharacterController component attached to the same GameObject
            controller = GetComponent<CharacterController>();
            
            // Get the main camera in the scene
            mainCamera = Camera.main;
            
            // Lock the cursor to the center of the screen and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Called every frame
        void Update()
        {
            // Handle player movement and rotation
            HandleMovement();
            HandleRotation();
        }

        // Handles player movement based on input
        void HandleMovement()
        {
            // Determine speed multiplier based on whether the Shift key is pressed
            float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? runMultiplier : 1f;

            // Get input for horizontal and vertical movement
            float horizontalSpeed = Input.GetAxis("Horizontal") * walkSpeed * speedMultiplier;
            float verticalSpeed = Input.GetAxis("Vertical") * walkSpeed * speedMultiplier;

            // Create a movement vector based on input
            Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
            
            // Rotate the movement vector based on player's current rotation
            horizontalMovement = transform.rotation * horizontalMovement;

            // Apply the movement to the CharacterController
            controller.SimpleMove(horizontalMovement);
        }

        // Handles player rotation based on mouse input
        private void HandleRotation()
        {
            // Get horizontal mouse input and rotate the player around the Y-axis
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, mouseX, 0);

            // Get vertical mouse input and rotate the camera vertically
            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Clamp the vertical rotation to prevent the player from looking too far up or down
            verticalRotation = Mathf.Clamp(verticalRotation, -yClamp, yClamp);

            // Apply the vertical rotation to the camera's local rotation
            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
}
