using UnityEngine;
using System.Diagnostics;

public class MainStopwatch : MonoBehaviour
{
    private TMPro.TMP_Text timerText;
    public float totalSeconds;

    private bool isStart;
    private bool isPause;
    private bool isReset;
    private bool isRunning;


    private System.TimeSpan stockTime;


    public void Init(float timeSpan)
    {
        totalSeconds = timeSpan;
        stockTime = System.TimeSpan.FromSeconds(totalSeconds);
    }

    public void Update()
    {
        // 시작
        if (isStart)
        {
            isStart = false;
            PlayStopwatch();
        }

        // 일시 정지
        if (isPause)
        {
            isPause = false;
            PauseStopwatch();
        }

        // 리셋
        if (isReset)
        {
            isReset = false;
            StopStopwatch();
        }

        if (isRunning)
        {
            stockTime = StockStopwatch();
            UpdateTimerText(stockTime);

        }
    }

    public void StopwatchInit(TMPro.TMP_Text timerText)
    {
        this.timerText = timerText;
    }

    public void PlayStopwatch()
    {
        isRunning = true;
    }

    public void PauseStopwatch()
    {
        isRunning = false;
    }

    public void ContinueStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
        timerText.text = "00:00:00";
    }

    private System.TimeSpan StockStopwatch()
    {
        totalSeconds += Time.deltaTime;
        System.TimeSpan elapsed = System.TimeSpan.FromSeconds(totalSeconds);
        return elapsed;
    }

    private void UpdateTimerText(System.TimeSpan elapsed)
    {
        if (isRunning)
        {
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
        }
    }
}
