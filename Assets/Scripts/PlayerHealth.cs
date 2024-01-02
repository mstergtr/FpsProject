using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SteamK12.FpsProject
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public int maxHealth = 10;
        public Slider healthSlider;
        public GameObject deathPrefab;
        private int currentHealth;

        private void Start()
        {           
            currentHealth = maxHealth;
            UpdateHealthUI();            
        }

        private void UpdateHealthUI()
        {
            float normalizedHealth = (float)currentHealth / maxHealth;
            healthSlider.value = normalizedHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                if (deathPrefab != null) Instantiate(deathPrefab, transform.position, transform.rotation);
                GameManager.Instance.GameLost();
            }
        }
    }
}
