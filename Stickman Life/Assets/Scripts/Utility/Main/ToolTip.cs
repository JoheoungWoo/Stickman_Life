

[System.Serializable]
public class ToolTip
{
    [System.Serializable]
    public struct ToolTipData
    {
        public medicineItemName medicineItemName;
        public fooditemName fooditemName;
        public DiseaseName diseaseName;
        public StatusName statusName;
        public ButtonName buttonName;

        public string context;

        #region Ÿ��Ʋ�� ���ؽ�Ʈ ��ȯ
        public string GetTitle()
        {
            var tempString = "";
            if (medicineItemName != medicineItemName.None)
            {
                tempString = medicineItemName.ToString();
            }
            else if (fooditemName != fooditemName.None)
            {
                tempString = fooditemName.ToString();
            }
            else if (diseaseName != DiseaseName.None)
            {
                tempString = diseaseName.ToString();
            }
            else if (statusName != StatusName.None)
            {
                tempString = statusName.ToString();
            }
            else if (buttonName != ButtonName.None)
            {
                tempString = buttonName.ToString();
            }
            return tempString;
        }

        public string GetContext()
        {
            return context;
        }

        public (string type, string data) GetAllData()
        {
            var context = GetContext();
            return (GetTitle(), string.IsNullOrEmpty(context) ? "DataManager���� ������ �Է����ּ���." : context);
        }
        #endregion
    }

    public ToolTipData[] toolTipData;

    public ToolTipData FindToolTipData(string findItemName)
    {
        foreach (var data in toolTipData)
        {
            if (data.GetTitle() == findItemName)
            {
                return data;
            }
        }
        return default;
    }
}