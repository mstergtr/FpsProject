using UnityEngine;

namespace SteamK12.FpsProject
{
    public class AddImpulseForce : MonoBehaviour
    {
        public Rigidbody rb;
        public float force = 5.0f;
        void Start()
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
