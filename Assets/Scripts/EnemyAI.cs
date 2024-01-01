using UnityEngine;
using UnityEngine.AI;

namespace SteamK12.FpsProject
{
    public class EnemyAI : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;

        void Update()
        {
            if (GameManager.Instance.PlayerTransform == null) return;
            FollowPlayer();
        }

        void FollowPlayer()
        {
            navMeshAgent.SetDestination(GameManager.Instance.PlayerTransform.position);
        }
    }
}
