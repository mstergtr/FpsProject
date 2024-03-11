using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SteamK12.FpsProject
{
    public class Crate : MonoBehaviour, IInteractable
    {
        public int health = 3;
        public GameObject deathPrefab;
        public bool isExplosive = false;
        public int expDamage = 10;
        public float expRadius = 4.0f;
        public Collider hitbox;

        bool isSnapped;
        float distFromPlayer = 2.0f;
        Rigidbody rb;

        private void Start() 
        {
            rb = GetComponent<Rigidbody>();
        }

        public void TakeDamage(int damage)
        {
            if (!isExplosive) return;
            
            health -= damage;

            if (health < 1)
            {          
                hitbox.enabled = false; //important for not creating infinite loop
                Explode();          
            }
        }

        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, expRadius);

            foreach (var nearbyObject in colliders)
            {
                IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(expDamage);
                }
            }
            Instantiate(deathPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        public void Interact(Transform player)
        {
            if (isSnapped)
            {
                transform.parent = null;
                rb.isKinematic = false;
                isSnapped = false;
            }
            else
            {
                transform.position = player.position + player.forward * distFromPlayer;
                transform.parent = player;
                rb.isKinematic = true;
                isSnapped = true;
            }       
        }

        #if UNITY_EDITOR
        // Draw the blast radius gizmo in the Unity Editor
        void OnDrawGizmos()
        {
            Handles.color = new Color(1f, 0.5f, 0f, 0.2f); // Set color with transparency
            Handles.DrawSolidDisc(transform.position, Vector3.up, expRadius);
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, expRadius);
        }
        #endif
    }
}
