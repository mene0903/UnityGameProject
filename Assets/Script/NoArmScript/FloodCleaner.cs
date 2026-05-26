using System.Collections;
using UnityEngine;

public class FloodCleaner : MonoBehaviour
{
    [Header("물 오브젝트")]
    public GameObject floodWaterObject;
    public SpriteRenderer floodRenderer;

    [Header("연출 시간")]
    public float fadeInTime = 0.7f;
    public float stayTime = 1.0f;
    public float fadeOutTime = 0.8f;

    [Header("물 투명도")]
    [Range(0f, 1f)]
    public float maxAlpha = 0.35f;

    [Header("제거할 웅덩이 태그")]
    public string toxicTag = "ToxicPuddle";

    private bool isRunning = false;
    private bool alreadyCleaned = false;

    void Start()
    {
        HideWaterImmediately();
    }

    public void StartFloodClean()
    {
        if (isRunning) return;
        if (alreadyCleaned) return;

        StartCoroutine(FloodRoutine());
    }

    IEnumerator FloodRoutine()
    {
        isRunning = true;
        alreadyCleaned = true;

        floodWaterObject.SetActive(true);

        yield return StartCoroutine(FadeWater(0f, maxAlpha, fadeInTime));

        RemoveToxicPuddles();

        yield return new WaitForSeconds(stayTime);

        yield return StartCoroutine(FadeWater(maxAlpha, 0f, fadeOutTime));

        floodWaterObject.SetActive(false);

        isRunning = false;
    }

    IEnumerator FadeWater(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            Color c = floodRenderer.color;
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            floodRenderer.color = c;

            yield return null;
        }

        Color finalColor = floodRenderer.color;
        finalColor.a = endAlpha;
        floodRenderer.color = finalColor;
    }

    void HideWaterImmediately()
    {
        if (floodRenderer != null)
        {
            Color c = floodRenderer.color;
            c.a = 0f;
            floodRenderer.color = c;
        }

        if (floodWaterObject != null)
            floodWaterObject.SetActive(false);
    }

    void RemoveToxicPuddles()
    {
        GameObject[] puddles = GameObject.FindGameObjectsWithTag(toxicTag);

        foreach (GameObject puddle in puddles)
        {
            puddle.SetActive(false);
        }
    }
}