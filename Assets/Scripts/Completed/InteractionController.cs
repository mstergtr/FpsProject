using UnityEngine;

namespace SteamK12.FpsProject
{
    public class InteractionController : MonoBehaviour
    {
        public float interactDistance = 3f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }
        }

        void TryInteract()
        {
            // Raycast from the center of the camera
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact(transform);
                }
            }
        }
    }
}
