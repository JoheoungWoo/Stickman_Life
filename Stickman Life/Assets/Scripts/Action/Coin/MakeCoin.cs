using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum coinName {bronze, silver, gold }

public class MakeCoin : MonoBehaviour
{
    public GameObject coinMakeRange;

    public float nowTimer = 0.0f;
    public float maxTImer = 1.0f;

    public GameObject[] coins;

    public float coinXMin = -4f;
    public float coinXMax = 4f;
    public float coinY = 7f;

    //코인 드롭률 조종
    public int coinDrop = 9;

    private void Update()
    {
        nowTimer += Time.deltaTime;
        if(nowTimer >= maxTImer)
        {
            nowTimer = 0.0f;
            if((int)Random.Range(0,100) <= coinDrop)
            {
                DropCoin();
            }
        }
    }

    public void DropCoin()
    {
        switch ((int)Random.Range(0, 10))
        {
            case 0:
                CreateCoin(coinName.gold);
                break;
            case 1:
            case 2:
            case 3:
                CreateCoin(coinName.silver);
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                CreateCoin(coinName.bronze);
                break;
            default:
                break;

        }
    }

    private void CreateCoin(coinName coin)
    {
        // Get the RectTransform of the coin range
        RectTransform coinRangeRect = coinMakeRange.GetComponent<RectTransform>();

        // Calculate the range based on the RectTransform
        float right = coinRangeRect.rect.width / 2;
        float left = -right;
        float top = coinRangeRect.rect.height / 2;
        float bottom = -top;

        // Randomly generate position within the range
        var position = new Vector2(Random.Range(left, right), Random.Range(bottom, top));

        switch (coin)
        {
            case coinName.bronze:
                var objBronze = Instantiate(coins[(int)coinName.bronze], transform);
                objBronze.name = "CoinBronze";
                objBronze.GetComponent<RectTransform>().anchoredPosition = position;
                break;
            case coinName.silver:
                var objSilver = Instantiate(coins[(int)coinName.silver], transform);
                objSilver.name = "CoinSilver";
                objSilver.GetComponent<RectTransform>().anchoredPosition = position;
                break;
            case coinName.gold:
                var objGold = Instantiate(coins[(int)coinName.gold], transform);
                objGold.name = "CoinGold";
                objGold.GetComponent<RectTransform>().anchoredPosition = position;
                break;
            default:
                break;
        }
    }
}
