using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    public GameObject itemPrefab;
    public bool isLoad = false;     //로드가 됬는지 확인

    private void OnEnable()
    {
        if(DataManager.Instance.mainData.itemList == null)
        {
            //Init();
        }
    }

    public void Init(List<EatData> eatDatas = null)
    {
        var myDatas = eatDatas;
        if(eatDatas == null)
        {
            myDatas = DataManager.Instance.mainData.itemList;
        }

        if (!isLoad)
        {
            foreach (var data in myDatas)
            {
                EatAddItem(itemPrefab, gameObject.transform, data);
            }
            isLoad = true;
            Debug.Log("FoodItem 로딩 완료");
        } else
        {
            Debug.Log("FoodItem은 이미 로딩됨");
        }
    }



}
