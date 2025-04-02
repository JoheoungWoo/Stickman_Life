using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseItem : Item
{
    public GameObject itemPrefab;
    public bool isLoad = false;     //로드가 됬는지 확인

    private void OnEnable()
    {
        if (DataManager.Instance.mainData.itemList == null)
        {
            //Init();
        }
    }

    public void Init(List<DiseaseData> diseaseDatas = null)
    {
        var myDatas = diseaseDatas;
        if (diseaseDatas == null)
        {
            myDatas = DataManager.Instance.mainData.diseaseItemList;
        }

        if (!isLoad)
        {
            foreach (var data in myDatas)
            {
                DiseaseAddItem(itemPrefab, gameObject.transform, data);
            }
            isLoad = true;
            Debug.Log("DiseaseItem 로딩 완료");
        } else
        {
            Debug.Log("DiseaseItem은 이미 로딩됨");
        }
    }
}
