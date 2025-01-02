using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public EnemyAI enemyAI;
        public float attackRange = 4.0f;
        public float detectionRange = 10.0f;
        //public float verticalOffset = 1.0f;
        public int damage = 1;
        public float timeBetweenAttacks = 1.0f;
        public int maxHealth = 3;
        public GameObject deathPrefab;
        private float attackTimer = 0;
        private float distanceToPlayer;
        private int currentHealth;
        private bool isAlive = true;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {
            if (GameManager.Instance.PlayerTransform == null) return;

            distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerTransform.position);
            
            if (distanceToPlayer <= detectionRange || currentHealth < maxHealth)
            {
                Vector3 directionToPlayer = GameManager.Instance.PlayerTransform.position - transform.position;
                directionToPlayer.y = 0f; // Ensure the ray is cast along the horizontal plane

                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, 100.0f))
                {
                    Debug.DrawRay(transform.position + Vector3.up, directionToPlayer.normalized * attackRange, Color.green, 1.0f);

                    if (hit.collider.CompareTag("Player"))
                    {
                        enemyAI.currentState = EnemyAI.EnemyState.FollowPlayer;
                    }
                }             
            }
            else
            {
                enemyAI.currentState = EnemyAI.EnemyState.Patrol;
            }

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

            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, attackRange))
            {
                Debug.DrawRay(transform.position + Vector3.up, directionToPlayer.normalized * attackRange, Color.red, 1.0f);

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
            currentHealth -= damage;

            if (currentHealth <= 0)
            {              
                if (isAlive && deathPrefab != null)
                {
                    Instantiate(deathPrefab, transform.position, transform.rotation);
                }
                isAlive = false;
                Destroy(gameObject);
            }
        }
    }
}
