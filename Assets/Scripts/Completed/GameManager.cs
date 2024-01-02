using UnityEngine;

namespace SteamK12.FpsProject
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Transform PlayerTransform;

        void Start()
        {
            Instance = this;
        }

        void Update()
        {

        }

        public void GameLost()
        {
            print("Game Lost");
        }

        public void GameWon()
        {
            print("Game Won");
        }
    }
}
