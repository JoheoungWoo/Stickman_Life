using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float destroyTimer = 60f; // 60초 타이머

    private void Update()
    {

        // 타이머 감소
        destroyTimer -= Time.deltaTime;

        // 타이머가 0 이하이면 오브젝트 파괴
        if (destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void UpGold()
    {
        var coin = gameObject;
        switch (coin.name)
        {
            case "CoinBronze":
                DataManager.Instance.mainData.nowGold += 1;
                break;
            case "CoinSilver":
                DataManager.Instance.mainData.nowGold += 3;
                break;
            case "CoinGold":
                DataManager.Instance.mainData.nowGold += 5;
                break;
        }
        DataManager.Instance.uiManager.ChangeGoldText(DataManager.Instance.mainData.nowGold);
        DataManager.Instance.soundManager.PlayBgm1();
        Destroy(coin);
    }
}
