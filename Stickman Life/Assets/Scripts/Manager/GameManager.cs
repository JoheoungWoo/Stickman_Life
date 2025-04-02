using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //속도 제어
    public float timeScale = 1f;

    //게임 프레임 고정
    private int frameRate = 60;

    //이벤트 활용
    private bool isDataLoad;        //데이터 로드 완료했는지
    private bool isStart;           //시작
    private bool isPasue;        //일시정지

    public bool IsPause => isPasue;

    #region 루프할 요소들 객체
    public Body body { get; private set; }
    public Disease disease { get; private set; }
    public Food food { get; private set; }
    public Gold gold { get; private set; }
    public Health health { get; private set; }
    public Level level { get; private set; }
    public Life life { get; private set; }
    public Mental mental { get; private set; }
    #endregion


    public void InitGame(MainData mainData)
    {
        body = new Body(mainData);
        disease = new Disease(mainData);
        food = new Food(mainData);
        gold = new Gold(mainData);
        health = new Health(mainData);
        level = new Level(mainData);
        life = new Life(mainData);
        mental = new Mental(mainData);

        isStart = true;

        Invoke("SetTargetFrameRate",2f);
    }

    public void PauseGame()
    {
        isPasue = true;
    }

    public void ContinueGame()
    {
        isPasue = false;
    }

    public void RestartGame()
    {

    }

    public void StopGame()
    {

    }

    public void SetTargetFrameRate()
    {
        Application.targetFrameRate = frameRate;
    }
    private void Update()
    {
        if (isPasue)
        {
            Time.timeScale = 0;
            return;
        }


        if (!isStart)
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = timeScale;

        level.Play();

        //순서대로
        gold.Play();
        disease.Play();
        life.Play();
        health.Play();
        body.Play();
        mental.Play();
        food.Play();            
    }
}
