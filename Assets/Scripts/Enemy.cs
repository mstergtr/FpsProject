using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float attackRange = 4.0f;
        public float verticalOffset = 1.0f;
        public int damage = 1;
        public float timeBetweenAttacks = 1.0f;
        public int health = 3;
        public GameObject deathPrefab;
        private float attackTimer = 0;
        private float distanceToPlayer;

        void Update()
        {
            if (GameManager.Instance.PlayerTransform == null) return;

            distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerTransform.position);

            if (distanceToPlayer <= attackRange && attackTimer > timeBetweenAttacks)
            {
                Attack();
            }

            attackTimer += Time.deltaTime;
        }

        void Attack()
        {
            Vector3 directionToPlayer = GameManager.Instance.PlayerTransform.position - transform.position;
            directionToPlayer.y = 0f; // Ensure the ray is cast along the horizontal plane

            if (Physics.Raycast(transform.position + Vector3.up * verticalOffset, directionToPlayer.normalized, out RaycastHit hit, attackRange))
            {
                Debug.DrawRay(transform.position + Vector3.up * verticalOffset, directionToPlayer.normalized * attackRange, Color.red, 1.0f);

                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null && hit.transform.CompareTag("Player"))
                {
                    damageable.TakeDamage(damage);
                    attackTimer = 0;
                }
            }           
        }


        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                if (deathPrefab != null) Instantiate(deathPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
