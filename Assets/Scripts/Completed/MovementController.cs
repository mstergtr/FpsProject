using UnityEngine;

namespace SteamK12.FpsProject
{
    public class MovementController : MonoBehaviour
    {
        public float walkSpeed = 3.0f;
        public float runMultiplier = 2.0f;
        public float jumpForce = 5.0f;
        public float gravity = 9.81f;

        public float mouseSense = 2.0f;
        public float yClamp = 80.0f;

        public AudioSource footstepSource;
        public AudioClip[] footstepClips;
        public float walkStepInterval = 0.5f;
        public float runStepInterval = 0.25f;
        public float minVelocity = 0.1f;

        private CharacterController controller;
        private float verticalRotation;
        private Camera mainCamera;
        private Vector3 currentMovement = Vector3.zero;
        private float footstepTimer;
        private float currentStepInterval;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            mainCamera = Camera.main;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleFootsteps();
        }

        private void HandleFootsteps()
        {
            bool isMoving = controller.velocity.magnitude > minVelocity;

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

        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSense;
            transform.Rotate(0, mouseX, 0);

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSense;

            verticalRotation = Mathf.Clamp(verticalRotation, -yClamp, yClamp);

            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        void HandleMovement()
        {
            float speedMult = Input.GetKey(KeyCode.LeftShift) ? runMultiplier : 1f;
            float horizontalInput = Input.GetAxis("Horizontal") * walkSpeed * speedMult;
            float verticalInput = Input.GetAxis("Vertical") * walkSpeed * speedMult;

            Vector3 horizontalMovement = new Vector3(horizontalInput, 0, verticalInput);
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
    }
}
