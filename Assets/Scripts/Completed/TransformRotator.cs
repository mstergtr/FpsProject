using UnityEngine;

namespace SteamK12.FpsProject
{
    public class TransformRotator : MonoBehaviour
    {
        public Vector3 rotationAmount = new Vector3(0, 0, 0f);
        public Space relativeTo = Space.Self;

        void Update()
        {
            transform.Rotate(rotationAmount * Time.deltaTime, relativeTo);
        }
    }
}

