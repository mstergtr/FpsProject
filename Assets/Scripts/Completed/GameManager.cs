using UnityEngine;

namespace SteamK12.FpsProject
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Transform PlayerTransform;
        public MovementController movementController;
        public WeaponController weaponController;
        public CanvasGroup canvasGroupWin;
        public CanvasGroup canvasGroupLose;
        public GameObject buttons;
        public float fadeDuration = 1.0f;
        private bool playerWon;
        private bool playerLose;
        private bool isGameOver;
        private float timer;

        void Start()
        {
            Instance = this;
        }

        void Update()
        {
            if (playerWon)
            {
                timer += Time.deltaTime;

                canvasGroupWin.alpha = timer / fadeDuration;

                if (fadeDuration > timer)
                {
                    buttons.SetActive(true);
                }
            }
            else if (playerLose)
            {
                timer += Time.deltaTime;

                canvasGroupLose.alpha = timer / fadeDuration;

                if (fadeDuration > timer)
                {
                    buttons.SetActive(true);
                }
            }
        }

        public void GameLost()
        {
            if (isGameOver) return;
            playerLose = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            movementController.enabled = false;
            weaponController.enabled = false;
            isGameOver = true;
        }

        public void GameWon()
        {
            if (isGameOver) return;
            playerWon = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            movementController.enabled = false;
            weaponController.enabled = false;
            isGameOver = true;
        }
    }
}
