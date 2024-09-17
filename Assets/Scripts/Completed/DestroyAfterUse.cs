using UnityEngine;

namespace SteamK12.FpsProject
{
    public class DestroyAfterUse : MonoBehaviour
    {
        public float lifeTime = 1.0f;
        void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}