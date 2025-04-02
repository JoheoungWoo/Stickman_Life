using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerStatusImg
{
    public Sprite playerDeathSprite;
    public Sprite playerDiseaseSprite;
    public Sprite playerLowBodySprite;
    public Sprite playerLowHealthSprite;
    public Sprite playerDefaultSprite;
}

public class Player : MonoBehaviour
{
    //�÷��̾� ��������Ʈ
    private Image charImg;

    //�÷��̾� ���� ��ȭ ��������Ʈ
    public PlayerStatusImg playerSprites;

    private void Awake()
    {
        charImg = GetComponent<Image>();
    }

    #region �÷��̾� ��� ����
    private void ChangePlayerImg(Sprite sprite)
    {
        //Debug.Log($"{sprite.name} ��������Ʈ �̸���!");
        charImg.sprite = sprite;
    }

    public void ChangePlayerDeathImg()
    {
        ChangePlayerImg(playerSprites.playerDeathSprite);
    }

    public void ChangePlayerDiseaseImg()
    {
        ChangePlayerImg(playerSprites.playerDiseaseSprite);
    }

    public void ChangePlayerLowBodyImg()
    {
        ChangePlayerImg(playerSprites.playerLowBodySprite);
    }

    public void ChangePlayerLowHealthImg()
    {
        ChangePlayerImg(playerSprites.playerLowHealthSprite);
    }

    public void ChangePlayerDefaultImg()
    {
        ChangePlayerImg(playerSprites.playerDefaultSprite);
    }
    #endregion
}
