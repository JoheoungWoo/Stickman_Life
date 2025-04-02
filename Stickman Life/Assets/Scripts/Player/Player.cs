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
    //플레이어 스프라이트
    private Image charImg;

    //플레이어 상태 변화 스프라이트
    public PlayerStatusImg playerSprites;

    private void Awake()
    {
        charImg = GetComponent<Image>();
    }

    #region 플레이어 모습 변경
    private void ChangePlayerImg(Sprite sprite)
    {
        //Debug.Log($"{sprite.name} 스프라이트 이름임!");
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
