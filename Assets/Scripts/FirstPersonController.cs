using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 3.0f;
        public float runMultipier = 2.0f;
        public float jumpForce = 5.0f;
        public float gravity = 9.81f;

        [Header("Mouse Settings")]
        public float mouseSensitivity = 2.0f;
        public float yClamp = 80.0f;

        [Header("Footsteps Settings")]
        public AudioSource footstepSource;
        public AudioClip[] footstepClips;
        public float walkStepInterval = 0.5f;
        public float runStepInterval = 0.25f;
        public float velocityThreshold = 0.1f;

        private CharacterController controller;
        private float verticalRotation;
        private Camera mainCamera;
        private Vector3 currentMovement = Vector3.zero;
        private float footstepTimer;
        private float currentStepInterval;


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
            HandleFootsteps();
        }

        void HandleMovement()
        {
            float speedMultiper = Input.GetKey(KeyCode.LeftShift) ? runMultipier : 1f;
            float horizontalSpeed = Input.GetAxis("Horizontal") * walkSpeed * speedMultiper;
            float verticalSpeed = Input.GetAxis("Vertical") * walkSpeed * speedMultiper;

            Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
            horizontalMovement = transform.rotation * horizontalMovement;

            HandleJumping();

            currentMovement.x = horizontalMovement.x;
            currentMovement.z = horizontalMovement.z;

            controller.Move(currentMovement * Time.deltaTime);
        }

        private void HandleJumping()
        {
            if (controller.isGrounded)
            {
                currentMovement.y = -0.5f;

                if (Input.GetButtonDown("Jump"))
                {
                    currentMovement.y = jumpForce;
                }
            }
            else
            {
                currentMovement.y -= gravity * Time.deltaTime;
            }
        }

        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, mouseX, 0);

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -yClamp, yClamp);

            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        void HandleFootsteps()
        {
            bool isMoving = controller.velocity.magnitude > velocityThreshold;

            if (isMoving && Input.GetKey(KeyCode.LeftShift))
            {
                currentStepInterval = runStepInterval;
            }
            else
            {
                currentStepInterval = walkStepInterval;
            }

            footstepTimer += Time.deltaTime;

            if (footstepTimer >= currentStepInterval && controller.isGrounded && isMoving)
            {
                footstepSource.PlayOneShot(footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)]);
                footstepTimer = 0.0f;
            }
        }
    }
}

