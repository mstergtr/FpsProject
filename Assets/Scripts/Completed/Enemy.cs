using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public EnemyAI enemyAI;
        public float attackRange = 4.0f;
        public float detectionRange = 10.0f;
        public float chaseTime = 3.0f;
        public int damage = 1;
        public float timeBetweenAttacks = 1.0f;
        public int maxHealth = 3;
        public GameObject deathPrefab;
        private float attackTimer;
        private float chaseTimer;
        private float distanceToPlayer;
        private int currentHealth;
        private bool isAlive = true;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerTransform.position);

            bool playerInDetectionRange = distanceToPlayer < detectionRange;
            bool playerInView = false;

            // Check if the player is visible once in range
            if (playerInDetectionRange)
            {
                Vector3 directionToPlayer = GameManager.Instance.PlayerTransform.position - transform.position;
                directionToPlayer.y = 0f; // Ensure the ray is cast along the horizontal plane

                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out RaycastHit hit, detectionRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerInView = true; // Player is in view
                    }
                }
            }

            // Transition logic
            if (currentHealth < maxHealth)
            {
                // If damaged, always follow the player
                enemyAI.currentState = EnemyAI.EnemyState.FollowPlayer;
                chaseTimer = 0; // Reset chase timer when damaged
            }
            else if (playerInView)
            {
                // If player is in view, start following
                enemyAI.currentState = EnemyAI.EnemyState.FollowPlayer;
                chaseTimer = 0; // Reset chase timer when player is in view
            }
            else if (enemyAI.currentState == EnemyAI.EnemyState.FollowPlayer)
            {
                // If following but the player is not in view, start the chase timer
                chaseTimer += Time.deltaTime;

                if (chaseTimer >= chaseTime)
                {
                    // Return to patrol if the player is not in view and chase timer expires
                    enemyAI.currentState = EnemyAI.EnemyState.Patrol;
                    chaseTimer = 0; // Reset chase timer when returning to patrol
                }
            }
            else
            {
                // Default to patrol state
                enemyAI.currentState = EnemyAI.EnemyState.Patrol;
                chaseTimer = 0; // Ensure chase timer is reset in patrol state
            }

            // Attack logic
            if (distanceToPlayer <= attackRange && attackTimer >= timeBetweenAttacks)
            {
                Attack();
            }

            // Increment attack timer
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
