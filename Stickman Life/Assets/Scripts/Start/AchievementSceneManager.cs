using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSceneManager : MonoBehaviour
{
    // 업적별 bool 값
    public bool Achieve01;
    public bool Achieve02;
    public bool Achieve03;
    public bool Achieve04;
    public bool Achieve05;
    public bool Achieve06;
    public bool Achieve07;
    public bool Achieve08;
    public bool Achieve09;
    public bool Achieve10;

    // 업적 온오프용 오브젝트
    public GameObject Achieve001;
    public GameObject Achieve002;
    public GameObject Achieve003;
    public GameObject Achieve004;
    public GameObject Achieve005;
    public GameObject Achieve006;
    public GameObject Achieve007;
    public GameObject Achieve008;
    public GameObject Achieve009;
    public GameObject Achieve010;

    public bool InitAchieve(string key)
    {
        //수정사항 프리프 값이 1이여야만 적용가능
        if (PlayerPrefs.HasKey(key) == true)
        {
            //AchieveXX값이 1이여먄 클리어로 판정
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }
        else
        {
            //데이터가 없을 경우 0으로 초기화
            PlayerPrefs.SetInt(key, 0);
            return false;
        }
    }

    public void Awake()
    {
        Achieve01 = InitAchieve("Achieve01");
        Achieve02 = InitAchieve("Achieve02");
        Achieve03 = InitAchieve("Achieve03");
        Achieve04 = InitAchieve("Achieve04");
        Achieve05 = InitAchieve("Achieve05");
        Achieve06 = InitAchieve("Achieve06");
        Achieve07 = InitAchieve("Achieve07");
        Achieve08 = InitAchieve("Achieve08");
        Achieve09 = InitAchieve("Achieve09");
        Achieve10 = InitAchieve("Achieve10");
    }

    public void Update()
    {
        if(Achieve01 == true)
            Achieve001.gameObject.SetActive(false);
        else
            Achieve001.gameObject.SetActive(true);

        if (Achieve02 == true)
            Achieve002.gameObject.SetActive(false);
        else
            Achieve002.gameObject.SetActive(true);

        if (Achieve03 == true)
            Achieve003.gameObject.SetActive(false);
        else
            Achieve003.gameObject.SetActive(true);

        if (Achieve04 == true)
            Achieve004.gameObject.SetActive(false);
        else
            Achieve004.gameObject.SetActive(true);

        if (Achieve05 == true)
            Achieve005.gameObject.SetActive(false);
        else
            Achieve005.gameObject.SetActive(true);

        if (Achieve06 == true)
            Achieve006.gameObject.SetActive(false);
        else
            Achieve006.gameObject.SetActive(true);

        if (Achieve07 == true)
            Achieve007.gameObject.SetActive(false);
        else
            Achieve007.gameObject.SetActive(true);

        if (Achieve08 == true)
            Achieve008.gameObject.SetActive(false);
        else
            Achieve008.gameObject.SetActive(true);

        if (Achieve09 == true)
            Achieve009.gameObject.SetActive(false);
        else
            Achieve009.gameObject.SetActive(true);

        if (Achieve10 == true)
            Achieve010.gameObject.SetActive(false);
        else
            Achieve010.gameObject.SetActive(true);
    }
}
