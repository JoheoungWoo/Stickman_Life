using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public float tempTimer;

    public List<int> statusUpList;

    public void EatAddItem(GameObject itemPrefab,Transform transform,EatData eatData)
    {
        statusUpList.Add(eatData.itemPrice);
        var obj = Instantiate(itemPrefab,transform);
        obj.name = itemPrefab.name;
        obj.transform.GetChild(0).GetComponent<Image>().sprite = eatData.itemImage;
        obj.transform.GetChild(1).GetComponent<Image>();
        obj.transform.GetChild(2).GetComponent<TMP_Text>().text = eatData.myItemName.ToString();
        obj.transform.GetChild(3).GetComponent<TMP_Text>().text = eatData.itemPrice.ToString();
    }

    public void DiseaseAddItem(GameObject itemPrefab, Transform transform, DiseaseData eatData)
    {
        statusUpList.Add(eatData.itemPrice);
        var obj = Instantiate(itemPrefab, transform);
        obj.name = itemPrefab.name;
        obj.transform.GetChild(0).GetComponent<Image>().sprite = eatData.itemImage;
        obj.transform.GetChild(1).GetComponent<Image>();
        obj.transform.GetChild(2).GetComponent<TMP_Text>().text = eatData.myItemName.ToString();
        obj.transform.GetChild(3).GetComponent<TMP_Text>().text = eatData.itemPrice.ToString();
    }
}
