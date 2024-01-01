using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Health : MonoBehaviour, IDamageable
    {
        public int health = 3;
        public GameObject deathPrefab;

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                if (deathPrefab != null) Instantiate(deathPrefab, transform.position, transform.rotation);
                GameManager.Instance.GameLost();
            }
        }
    }
}
