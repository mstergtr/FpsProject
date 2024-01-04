using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace SteamK12.FpsProject
{
    public class Timer : MonoBehaviour
    {
        public TextMeshProUGUI timerText;
        public float time = 10.0f;
        public bool timerIsRunning;
        public UnityEvent onTimerEnd;

        private float currentTime;

        private void Start()
        {
            currentTime = time;
        }

        void Update()
        {
            if (timerIsRunning)
            {
                if (currentTime >= 0)
                {
                    currentTime -= Time.deltaTime;
                    DisplayTime(currentTime);
                    //currentTime = time;
                }
                else
                {
                    onTimerEnd.Invoke();
                    timerText.SetText("");
                    timerIsRunning = false;
                }
            }
        }

        public void SetTimer(bool timerState)
        {
            timerIsRunning = timerState;
            currentTime = time;
        }

        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
    }
}
