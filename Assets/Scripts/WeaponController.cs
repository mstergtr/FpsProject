using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SteamK12.FpsProject
{
    public class WeaponController : MonoBehaviour
    {
        public int damage = 10;
        public Transform cam;
        public ParticleSystem muzzleVFX;
        public GameObject hitVFX;
        public int ammo = 30;
        public TextMeshProUGUI ammoText;
        public float reloadTime = 2f;
        public AudioSource shootSource;
        public AudioSource reloadSource;
        public AudioClip[] shootSounds;
        public AudioClip reloadSound;

        private int currentAmmo;
        private bool isReloading = false;
        private float reloadTimer = 0f;

        private void Start()
        {
            currentAmmo = ammo;
            ammoText.text = "Ammo: " + currentAmmo;
        }
        void Update()
        {
            if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                Reload();
            }

            // Update the reload timer if reloading
            if (isReloading)
            {
                reloadTimer -= Time.deltaTime;

                if (reloadTimer <= 0f)
                {
                    // Reset ammo and reloading state when the timer reaches 0
                    currentAmmo = ammo;
                    ammoText.text = "Ammo: " + currentAmmo;
                    isReloading = false;
                }
            }
        }

        void Shoot()
        {
            currentAmmo--;
            ammoText.text = "Ammo: " + currentAmmo;

            shootSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);

            muzzleVFX.Play();

            // Raycast to detect hits
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit))
            {
                // Deal damage to the hit target
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                }

                // Instantiate hit VFX at the hit point
                Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

        void Reload()
        {
            isReloading = true;           
            reloadTimer = reloadTime;

            reloadSource.PlayOneShot(reloadSound);
        }
    }
}
