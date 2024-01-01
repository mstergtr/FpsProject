using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Crate : MonoBehaviour, IInteractable
    {
        private bool isSnapped = false;
        private Vector3 originalPosition;
        private float distanceFromPlayer = 2f;
        private Rigidbody crateRigidbody;

        void Start()
        {
            originalPosition = transform.position;
            crateRigidbody = GetComponent<Rigidbody>();
        }

        public void Interact(Transform playerTransform)
        {
            if (isSnapped)
            {
                // Unparent the crate and drop it
                transform.parent = null;
                crateRigidbody.isKinematic = false; // Allow physics to take over
                isSnapped = false;
            }
            else
            {
                // Snap the crate in front of the player
                transform.position = playerTransform.position + playerTransform.forward * distanceFromPlayer;
                transform.parent = playerTransform; // Parent the crate to the player
                crateRigidbody.isKinematic = true; // Make it kinematic while snapped
                isSnapped = true;
            }
        }
    }
}
