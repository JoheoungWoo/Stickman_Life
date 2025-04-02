using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    public GameObject itemPrefab;
    public bool isLoad = false;     //�ε尡 ����� Ȯ��

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
            Debug.Log("FoodItem �ε� �Ϸ�");
        } else
        {
            Debug.Log("FoodItem�� �̹� �ε���");
        }
    }



}
