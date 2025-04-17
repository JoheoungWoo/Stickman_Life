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
        // ��������� ���� AudioSource�� �߰��ϰ� �ʱ� BGM ����
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceBGM.clip = mainBgm;
        audioSourceBGM.loop = true; // ��������� �ݺ� ���
        audioSourceBGM.volume = 0.08f;

        // ��ư Ŭ�� ���带 ���� AudioSource�� �߰�
        audioSourceButtonClick = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Ư�� ���ǿ� ���� BGM ��ȯ�� ó���� �� �ֽ��ϴ�.
        // ���⼭�� ���÷� Input.GetKeyDown(KeyCode.Alpha1)�̳� �ٸ� ������ ����� �� �ֽ��ϴ�.

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
        // ���� BGM ���� �� mainBgm ���
        audioSourceBGM.Stop();
        audioSourceBGM.Play();
    }

    public void PlayBgm1()
    {
        // ���� BGM ���� �� bgm1 ���
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm1;
        audioSourceButtonClick.Play();
    }

    public void PlayBgm2()
    {
        // ���� BGM ���� �� bgm2 ���
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm2;
        audioSourceButtonClick.Play();
    }

    public void PlayBgm3()
    {
        // ���� BGM ���� �� bgm3 ���
        audioSourceButtonClick.Stop();
        audioSourceButtonClick.clip = bgm3;
        audioSourceButtonClick.Play();
    }
}
