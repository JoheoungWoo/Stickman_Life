using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseItem : Item
{
    public GameObject itemPrefab;
    public bool isLoad = false;     //�ε尡 ����� Ȯ��

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
            Debug.Log("DiseaseItem �ε� �Ϸ�");
        } else
        {
            Debug.Log("DiseaseItem�� �̹� �ε���");
        }
    }
}
