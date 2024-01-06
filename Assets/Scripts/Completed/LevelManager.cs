using UnityEngine;
using UnityEngine.SceneManagement;

namespace SteamK12.FpsProject
{
    public class LevelManager : MonoBehaviour
    {
        private int currentSceneIndex;

        private void Start() 
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadScene(string level)
        {
            SceneManager.LoadScene(level);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
