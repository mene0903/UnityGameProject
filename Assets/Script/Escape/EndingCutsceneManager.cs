using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCutsceneManager : MonoBehaviour
{
    [Header("컷씬 이미지")]
    public Image cutsceneImage;
    public Sprite[] endingSprites;

    [Header("시간 설정")]
    public float fadeTime = 1.5f;
    public float imageShowTime = 3f;

    [Header("마지막에 이동할 씬")]
    public string nextSceneName = "MainMenuScene";

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlayEndingCutscene());
    }

    IEnumerator PlayEndingCutscene()
    {
        for (currentIndex = 0; currentIndex < endingSprites.Length; currentIndex++)
        {
            cutsceneImage.sprite = endingSprites[currentIndex];

            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(imageShowTime);
            yield return StartCoroutine(FadeOut());
        }

        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        Color color = cutsceneImage.color;
        color.a = 0f;
        cutsceneImage.color = color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeTime);
            cutsceneImage.color = color;
            yield return null;
        }

        color.a = 1f;
        cutsceneImage.color = color;
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = cutsceneImage.color;
        color.a = 1f;
        cutsceneImage.color = color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            cutsceneImage.color = color;
            yield return null;
        }

        color.a = 0f;
        cutsceneImage.color = color;
    }
}