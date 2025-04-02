using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float destroyTimer = 60f; // 60�� Ÿ�̸�

    private void Update()
    {

        // Ÿ�̸� ����
        destroyTimer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0 �����̸� ������Ʈ �ı�
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
