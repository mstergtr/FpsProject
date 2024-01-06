using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Explosion : MonoBehaviour
    {
        public float blastRadius = 20f;
        public float expForce = 1000f;
        public float upwardsMod = 10f;
        public int damage = 50;
        public GameObject vfxPrefab;

        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(expForce, transform.position, blastRadius, upwardsMod);
                }

                IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }
            }
            Instantiate(vfxPrefab, transform.position, transform.rotation);
        }
    }
}

