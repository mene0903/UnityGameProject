using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathCutscenePlayer : MonoBehaviour
{
    [Header("순서대로 보여줄 죽음 이미지 3장")]
    public Sprite[] deathImages;

    [Header("이미지를 보여줄 UI Image")]
    public Image targetImage;

    [Header("이미지 한 장당 보여줄 시간")]
    public float imageDuration = 1.5f;

    [Header("마지막에 이동할 게임오버 씬 이름")]
    public string gameOverSceneName = "GameOverScene";

    private int currentIndex = 0;

    void Start()
    {
        if (deathImages == null || deathImages.Length == 0 || targetImage == null)
        {
            Debug.LogError("죽음 이미지 또는 Target Image가 연결되지 않았습니다.");
            return;
        }

        ShowCurrentImage();
        InvokeRepeating(nameof(ShowNextImage), imageDuration, imageDuration);
    }

    void ShowCurrentImage()
    {
        targetImage.sprite = deathImages[currentIndex];
        targetImage.preserveAspect = true;
    }

    void ShowNextImage()
    {
        currentIndex++;

        if (currentIndex >= deathImages.Length)
        {
            CancelInvoke(nameof(ShowNextImage));
            SceneManager.LoadScene(gameOverSceneName);
            return;
        }

        ShowCurrentImage();
    }
}   