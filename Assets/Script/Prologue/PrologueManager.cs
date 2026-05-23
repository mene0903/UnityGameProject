using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PrologueManager : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI subtitleText;

    public Sprite[] images;
    public string[] subtitles = {
        "로봇과 아이는 서로에게 가장 소중한 친구였다...",
        "평소와 같이 아이를 데리러 가는길에...",
        "그날, 모든 것이 산산조각 났다..."
    };

    private int currentIndex = 0;
    private bool isTransitioning = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextSlide();
        }
    }

    void Start()
    {
        ShowSlide(0);
    }

    void ShowSlide(int index)
    {
        backgroundImage.sprite = images[index];
        subtitleText.text = subtitles[index];
    }

    public void NextSlide()
    {
        if (isTransitioning) return;
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            SceneManager.LoadScene("JunkYardScene");
        }
        else
        {
            StartCoroutine(FadeTransition());
        }
    }

    IEnumerator FadeTransition()
    {
        isTransitioning = true;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            backgroundImage.color = new Color(1, 1, 1, 1f - t);
            yield return null;
        }
        ShowSlide(currentIndex);
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            backgroundImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
        isTransitioning = false;
    }
}