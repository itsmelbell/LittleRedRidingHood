using UnityEngine;
using UnityEngine.Events;
using TMPro;

//for bee and apple level
public class MinigameTimer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private bool startOnAwake = true;
    public UnityEvent onTimeUp;

    private float timeLeft;
    private bool running;

    private void Start(){
        timeLeft = timeLimit;
        running = startOnAwake;
        UpdateText();
    }

    private void Update(){
        if (!running || PauseController.IsGamePaused) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            running = false;
            UpdateText();
            onTimeUp?.Invoke();
            return;
        }

        UpdateText();
    }

    private void UpdateText(){
        int seconds = Mathf.CeilToInt(timeLeft);
        timerText.text = $"{seconds / 60}:{seconds % 60:00}";
    }

    public void ResetTimer(){
        timeLeft = timeLimit;
        running = true;
        UpdateText();
    }

    public void StartTimer() => running = true;   
    public void Stop() => running = false;     
}