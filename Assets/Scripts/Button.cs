using UnityEngine;
using UnityEngine.Events;
using GogoGaga.TME;

namespace SteamK12.FpsProject
{
    public class Button : MonoBehaviour, IInteractable
    {
        public UnityEvent onPressed;
        public LeantweenCustomAnimator pressedAnimation;
        public float cooldownTime = 2f;
        private float cooldownTimer = 0f;
        private bool isPressed = false;

        void Update()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        public void Interact(Transform playerTransform)
        {
            if (!isPressed)
            {
                onPressed.Invoke();
                pressedAnimation.PlayAnimation();
                isPressed = true;

                // Start the cooldown timer
                cooldownTimer = cooldownTime;

                // Reset the button after the cooldown period
                Invoke(nameof(ResetButton), cooldownTime);
            }
        }

        private void ResetButton()
        {
            isPressed = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null && cooldownTimer <= 0)
            {
                Interact(gameObject.transform);
            }
        }
    }
}

