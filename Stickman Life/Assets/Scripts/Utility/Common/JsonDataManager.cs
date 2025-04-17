using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    //플레이어 스테이터스
    public int retryCount;      //시도 횟수
    public string playerName;   //플레이어 이름
    public int lifePoint;       //생명력
    public int maxLifePoint;    //최대 생명력
    public int healthPoint;     //체력
    public int maxHealthPoint;  //최대 체력
    public int bodyPoint;       //건강
    public int maxBodyPoint;    //최대 건강
    public int mentalPoint;     //정신력
    public int maxMentalPoint;  //최대 정신력
    public int foodPoint;       //포만감
    public int maxFoodPoint;    //최대 포만감
    public int gold;            //골드량+
    public float timeSpan;      //진행한 시간

    //스킬 포인트
    public int increaseLifePoint;
    public int increaseHealthPoint;
    public int increaseHealthRecoveryPoint;
    public int increaseGoldProsperityPoint;

    //식중독 여부
    public bool isFoodPoisoning;
    public bool isHallucination;
    public bool isCold;
    public bool isCancer;

    public int exp;
    public int maxExp;
    public int level;

    //카운트
    //업적 체크를 위함
    public int dietarysupplementCount;

    public int foodPoisoningCount;
    public int hallucinationCount;
    public int coldCount;
    public int cancerCount;

    public int candyCount;
    public int chipCount;
    public int ramyunCount;
    public int kimbabCount;
    public int meatCount;


    public PlayerData()
    {

    }

    public PlayerData(int retryCount, string playerName, int lifePoint, int maxLifePoint, int healthPoint, int maxHealthPoint, int bodyPoint, int maxBodyPoint, int mentalPoint,
    int maxMentalPoint, int foodPoint, int maxFoodPoint, int gold, float timeSpan, int increaseLifePoint, int increaseHealthPoint, int increaseHealthRecoveryPoint,
       int increaseGoldProsperityPoint, bool isFoodPoisoning, bool isHallucination, bool isCold, bool isCancer, int exp, int maxExp, int level,
       int dietarysupplementCount, int foodPoisoningCount, int hallucinationCount, int coldCount, int cancerCount,
       int candyCount, int chipCount, int ramyunCount, int kimbabCount, int meatCount)
    {
        this.retryCount = retryCount;
        this.playerName = playerName;
        this.lifePoint = lifePoint;
        this.maxLifePoint = maxLifePoint;
        this.healthPoint = healthPoint;
        this.maxHealthPoint = maxHealthPoint;
        this.bodyPoint = bodyPoint;
        this.maxBodyPoint = maxBodyPoint;
        this.mentalPoint = mentalPoint;
        this.maxMentalPoint = maxMentalPoint;
        this.foodPoint = foodPoint;
        this.maxFoodPoint = maxFoodPoint;
        this.gold = gold;
        this.timeSpan = timeSpan;

        this.increaseLifePoint = increaseLifePoint;
        this.increaseHealthPoint = increaseHealthPoint;
        this.increaseHealthRecoveryPoint = increaseHealthRecoveryPoint;
        this.increaseGoldProsperityPoint = increaseGoldProsperityPoint;

        this.isFoodPoisoning = isFoodPoisoning;
        this.isHallucination = isHallucination;
        this.isCold = isCold;
        this.isCancer = isCancer;

        this.exp = exp;
        this.maxExp = maxExp;
        this.level = level;

        this.dietarysupplementCount = dietarysupplementCount;
        this.foodPoisoningCount = foodPoisoningCount;
        this.hallucinationCount = hallucinationCount;
        this.coldCount = coldCount;
        this.cancerCount = cancerCount;
        this.candyCount = candyCount;
        this.chipCount = chipCount;
        this.ramyunCount = ramyunCount;
        this.kimbabCount = kimbabCount;
        this.meatCount = meatCount;
    }

    public void ReadData()
    {
        Debug.Log($"Player Data: \nRetry Count: {this.retryCount}, \nPlayer Name: {this.playerName}, \nLife Point: {this.lifePoint}, \nHealth Point: {this.healthPoint}, \nBody Point: {this.bodyPoint}, \nMental Point: {this.mentalPoint}, \nFood Point: {this.foodPoint}, \nGold: {this.gold}, \ntimeSpan: {this.timeSpan}, \nIncrease Life Point: {this.increaseLifePoint}, \nIncrease Health Point: {this.increaseHealthPoint}, \nIncrease Health Recovery Point: {this.increaseHealthRecoveryPoint}, \nIncrease Gold Prosperity Point: {this.increaseGoldProsperityPoint}, \nIs Food Poisoning: {this.isFoodPoisoning}, \nIs Hallucination: {this.isHallucination}, \nIs Cold: {this.isCold}, \nIs Cancer: {this.isCancer}");
    }

    public MainData MainDataToPlayerData()
    {
        return new MainData(retryCount,playerName,lifePoint,maxLifePoint,healthPoint,maxHealthPoint,bodyPoint,maxBodyPoint,mentalPoint,
    maxMentalPoint,foodPoint,maxFoodPoint,gold, timeSpan, increaseLifePoint,increaseHealthPoint,increaseHealthRecoveryPoint,
       increaseGoldProsperityPoint,isFoodPoisoning,isHallucination,isCold,isCancer,exp,maxExp,level,
       dietarysupplementCount, foodPoisoningCount, hallucinationCount, coldCount, cancerCount, candyCount, chipCount, ramyunCount, kimbabCount,meatCount);
    }
}

public class JsonDataManager : MonoBehaviour
{
    public string GetFilePath()
    {
        string path = null;
        //아래는 접근 경로입니다. 

#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/JSON/";
#elif UNITY_ANDROID
        path = Application.persistentDataPath;
#endif
        return path;
    }

    public bool CheckExistFile(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");
        var result = File.Exists(path);
        return result;
    }

    
    public void ClearData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        File.Delete(path);
    }

    /// <summary>
    /// JSON 데이터 생성
    /// </summary>
    public void CreateData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");
        var data = new PlayerData();

        //JSON 문자열로 직렬화
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonString);

        Debug.Log($"JSON file created at: {path}");


        // 파일에서 JSON ggf데이터를 읽어와 역직렬화
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        // 역직렬화한 데이터를 출력
        readData.ReadData();

    }

    /// <summary>
    /// JSON데이터 초기화
    /// </summary>
    public void InitData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // 파일에서 JSON ggf데이터를 읽어와 역직렬화
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        //데이터 초기화
        readData.retryCount = 0;
        readData.playerName = "NoName";
        readData.lifePoint = 100;
        readData.maxLifePoint = 100;
        readData.healthPoint = 100;
        readData.maxHealthPoint = 100;
        readData.bodyPoint = 100;
        readData.maxBodyPoint = 100;
        readData.mentalPoint = 100;
        readData.maxMentalPoint = 100;
        readData.foodPoint = 75;
        readData.maxFoodPoint = 100;
        readData.gold = 100;
        readData.timeSpan = 0;
        readData.increaseLifePoint = 0;
        readData.increaseHealthPoint = 0;
        readData.increaseHealthRecoveryPoint = 0;
        readData.increaseGoldProsperityPoint = 0;
        readData.isFoodPoisoning = false;
        readData.isHallucination = false;
        readData.isCold = false;
        readData.isCancer = false;
        readData.exp = 0;
        readData.maxExp = 100;
        readData.level = 1;
        readData.dietarysupplementCount = 0;
        readData.foodPoisoningCount = 0;
        readData.hallucinationCount = 0;
        readData.coldCount = 0;
        readData.cancerCount = 0;
        readData.candyCount = 0;
        readData.chipCount = 0;
        readData.ramyunCount = 0;
        readData.kimbabCount = 0;
        readData.meatCount = 0;

        //JSON 문자열로 직렬화
        File.WriteAllText(path, JsonUtility.ToJson(readData));
    }

    public PlayerData LoadData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // 파일에서 JSON 데이터를 읽어와 역직렬화
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        // 역직렬화한 데이터를 출력
        readData.ReadData();

        return readData;
    }

    public void UpdateData(PlayerData playerData, string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // 파일에서 JSON 데이터를 읽어와 역직렬화
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        readData = playerData;

        //JSON 문자열로 직렬화
        File.WriteAllText(path, JsonUtility.ToJson(readData));
    }



}
