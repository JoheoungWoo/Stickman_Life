using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip mainBgm;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;

    private AudioSource audioSourceBGM;
    private AudioSource audioSourceButtonClick;

    void Awake()
    {
        // 배경음악을 위한 AudioSource를 추가하고 초기 BGM 설정
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceBGM.clip = mainBgm;
        audioSourceBGM.loop = true; // 배경음악은 반복 재생
        audioSourceBGM.volume = 0.08f;

        // 버튼 클릭 사운드를 위한 AudioSource를 추가
        audioSourceButtonClick = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // 특정 조건에 따라 BGM 전환을 처리할 수 있습니다.
        // 여기서는 예시로 Input.GetKeyDown(KeyCode.Alpha1)이나 다른 조건을 사용할 수 있습니다.

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayBgm1();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PlayBgm2();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            PlayBgm3();
        }
    }


    public void StopMainBgm()
    {
        audioSourceBGM.Stop();
    }
    public void PlayMainBgm()
    {
        // 기존 BGM 중지 및 mainBgm 재생
        audioSourceBGM.Stop();
        audioSourceBGM.Play();
    }

    public void PlayBgm1()
    {
        // 기존 BGM 중지 및 bgm1 재생
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm1;
        audioSourceButtonClick.Play();
    }

    public void PlayBgm2()
    {
        // 기존 BGM 중지 및 bgm2 재생
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm2;
        audioSourceButtonClick.Play();
    }

    public void PlayBgm3()
    {
        // 기존 BGM 중지 및 bgm3 재생
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm3;
        audioSourceButtonClick.Play();
    }
}
