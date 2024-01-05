using UnityEngine;
using UnityEngine.AI;

namespace SteamK12.FpsProject
{
    public class EnemyAI : MonoBehaviour
    {
        public enum EnemyState { None, FollowPlayer, Patrol }
        public EnemyState currentState;
        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;
        private int currentWaypointIndex;

        void Update()
        {
            if (GameManager.Instance.PlayerTransform == null) return;

            if (currentState == EnemyState.FollowPlayer || waypoints.Length < 1) FollowPlayer();
            else if (currentState == EnemyState.Patrol) Patrol();
            else currentState = EnemyState.None;
        }

        void FollowPlayer()
        {
            navMeshAgent.SetDestination(GameManager.Instance.PlayerTransform.position);
        }

        void Patrol()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }
}
