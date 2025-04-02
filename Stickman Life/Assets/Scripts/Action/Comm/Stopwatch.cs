using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    private TMPro.TMP_Text timerText;

    private float startTime;

    public bool isStart;
    public bool isPause;
    public bool isReset;

    public bool isRunning;

    public Stopwatch(TMPro.TMP_Text timerText)
    {
        this.timerText = timerText;
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
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StopwatchInit(TMPro.TMP_Text timerText)
    {
        this.timerText = timerText;
    }

    public void PlayStopwatch()
    {
        isRunning = true;
        startTime = Time.time;
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

    public void UpdateTimerText(float elapsedTime)
    {
    //경과 시간을 시, 분, 초로 변환하여 텍스트 업데이트
    timerText.text = string.Format("{0:00}:{1:00}:{2:00}", 
                                   (int)(elapsedTime / 3600f), // 시간
                                   (int)((elapsedTime / 60f) % 60), // 분
                                   (int)(elapsedTime % 60f)); // 초
    }
}
