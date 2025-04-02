using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StartFader : MonoBehaviour
{
    public Image Fade; // 페이드용 이미지
    float time = 0f;
    float f_time = 2f; // 페이드가 일어날 시간

    public void FadeOut()
    {
        StartCoroutine(Fade_Out());
    }

    public void FadeIn()
    {
        StartCoroutine(Fade_In());
    }

    IEnumerator Fade_Out()
    {
        Fade.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Fade.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Fade.color = alpha;
            yield return null;
        }

        /*
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Fade.color = alpha;
            yield return null;
        }
        */

        //Fade.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator Fade_In()
    {
        Fade.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Fade.color;

        /*
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Fade.color = alpha;
            yield return null;
        }
        */

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Fade.color = alpha;
            yield return null;
        }

        Fade.gameObject.SetActive(false);
        yield return null;
    }

    public void FadeOff()
    {
        Fade.gameObject.SetActive(false);
    }
}
