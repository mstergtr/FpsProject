using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class SimpleMovement : MonoBehaviour
    {
        [Header("Movement Speeds")]
        public float walkSpeed = 3.0f;
        public float runMultipier = 2.0f;

        [Header("Mouse Sensitivity")]
        public float mouseSensitivity = 2.0f;
        public float yClamp = 80.0f;

        private CharacterController controller;
        private float verticalRotation;
        private Camera mainCamera;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            mainCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        void HandleMovement()
        {
            float speedMultiper = Input.GetKey(KeyCode.LeftShift) ? runMultipier : 1f;
            float horizontalSpeed = Input.GetAxis("Horizontal") * walkSpeed * speedMultiper;
            float verticalSpeed = Input.GetAxis("Vertical") * walkSpeed * speedMultiper;

            Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
            horizontalMovement = transform.rotation * horizontalMovement;

            controller.SimpleMove(horizontalMovement);
        }

        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, mouseX, 0);

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -yClamp, yClamp);

            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
}
