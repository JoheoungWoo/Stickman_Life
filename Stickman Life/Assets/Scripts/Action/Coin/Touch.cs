using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour
{
    public Camera cam;
    public UIManager uiManager;
    public GameObject toolTipPrefab;
    public Transform toolTipParent;

    private float halfWidth; //클릭한 대상 크기 가져오기
    private float halfHeight; //클릭한 대상 크기 가져오기

    void Start()
    {
        cam = GetComponent<Camera>();
        toolTipPrefab = DataManager.Instance.toolTIpPrefab;
        toolTipParent = DataManager.Instance.toolTipParent;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // UI를 클릭한 경우
                GameObject clickedUI = EventSystem.current.currentSelectedGameObject;
                if (clickedUI != null)
                {
                    Debug.Log("UI를 인식하였읍니다: " + clickedUI.name);
                    Debug.Log("UI를 인식하였읍니다: " + clickedUI.tag);

                    switch (clickedUI.tag)
                    {
                        //질병류
                        case nameof(DiseaseName.FoodPoisoning):
                            uiManager.Tooltip05();
                            break;
                        case nameof(DiseaseName.Hallucination):
                            uiManager.Tooltip06();
                            break;
                        case nameof(DiseaseName.Cold):
                            uiManager.Tooltip07();
                            break;
                        case nameof(DiseaseName.Cancer):
                            uiManager.Tooltip08();
                            break;

                        //스테이터스류
                        case nameof(StatusName.Life):
                            uiManager.Tooltip00();
                            break;
                        case nameof(StatusName.Health):
                            uiManager.Tooltip02();
                            break;
                        case nameof(StatusName.Body):
                            uiManager.Tooltip01();
                            break;
                        case nameof(StatusName.Mental):
                            uiManager.Tooltip03();
                            break;
                        case nameof(StatusName.Food):
                            uiManager.Tooltip04();
                            break;
                        default:
                            
                            break;
                    }
                }
                return;
            }
        }
    }




}
