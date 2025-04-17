using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    //�÷��̾� �������ͽ�
    public int retryCount;      //�õ� Ƚ��
    public string playerName;   //�÷��̾� �̸�
    public int lifePoint;       //�����
    public int maxLifePoint;    //�ִ� �����
    public int healthPoint;     //ü��
    public int maxHealthPoint;  //�ִ� ü��
    public int bodyPoint;       //�ǰ�
    public int maxBodyPoint;    //�ִ� �ǰ�
    public int mentalPoint;     //���ŷ�
    public int maxMentalPoint;  //�ִ� ���ŷ�
    public int foodPoint;       //������
    public int maxFoodPoint;    //�ִ� ������
    public int gold;            //��差+
    public float timeSpan;      //������ �ð�

    //��ų ����Ʈ
    public int increaseLifePoint;
    public int increaseHealthPoint;
    public int increaseHealthRecoveryPoint;
    public int increaseGoldProsperityPoint;

    //���ߵ� ����
    public bool isFoodPoisoning;
    public bool isHallucination;
    public bool isCold;
    public bool isCancer;

    public int exp;
    public int maxExp;
    public int level;

    //ī��Ʈ
    //���� üũ�� ����
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
        //�Ʒ��� ���� ����Դϴ�. 

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
    /// JSON ������ ����
    /// </summary>
    public void CreateData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");
        var data = new PlayerData();

        //JSON ���ڿ��� ����ȭ
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonString);

        Debug.Log($"JSON file created at: {path}");


        // ���Ͽ��� JSON ggf�����͸� �о�� ������ȭ
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        // ������ȭ�� �����͸� ���
        readData.ReadData();

    }

    /// <summary>
    /// JSON������ �ʱ�ȭ
    /// </summary>
    public void InitData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // ���Ͽ��� JSON ggf�����͸� �о�� ������ȭ
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        //������ �ʱ�ȭ
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

        //JSON ���ڿ��� ����ȭ
        File.WriteAllText(path, JsonUtility.ToJson(readData));
    }

    public PlayerData LoadData(string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // ���Ͽ��� JSON �����͸� �о�� ������ȭ
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        // ������ȭ�� �����͸� ���
        readData.ReadData();

        return readData;
    }

    public void UpdateData(PlayerData playerData, string fileName)
    {
        var path = Path.Combine(GetFilePath(), $"{fileName}.json");

        // ���Ͽ��� JSON �����͸� �о�� ������ȭ
        string readJsonString = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<PlayerData>(readJsonString);

        readData = playerData;

        //JSON ���ڿ��� ����ȭ
        File.WriteAllText(path, JsonUtility.ToJson(readData));
    }



}
