using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Grenade : MonoBehaviour
    {
        public float blastRadius = 10.0f;
        public float expForce = 100.0f;
        public int damage = 100;
        public GameObject expPrefab;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) Explode();
        }

        private void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(expForce, transform.position, blastRadius);
                }

                IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }

            Instantiate(expPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
