using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneDeathCutscene : MonoBehaviour
{
    [Header("ÄÆ¾À UI")]
    public GameObject cutscenePanel;
    public Image cutsceneImage;

    [Header("ÇöÀç ¾À »ç¸Á ÀÌ¹ÌÁö 3Àå")]
    public Sprite[] deathImages;

    [Header("¼³Á¤")]
    public float imageDuration = 2f;
    public string gameOverSceneName = "GameOverScene";

    private bool isPlaying = false;

    void Start()
    {
        if (cutscenePanel != null)
            cutscenePanel.SetActive(false);
    }

    public void PlayDeathCutscene()
    {
        if (isPlaying) return;

        StartCoroutine(DeathCutsceneRoutine());
    }

    IEnumerator DeathCutsceneRoutine()
    {
        isPlaying = true;

        Time.timeScale = 0f;

        cutscenePanel.SetActive(true);

        for (int i = 0; i < deathImages.Length; i++)
        {
            cutsceneImage.sprite = deathImages[i];
            yield return new WaitForSecondsRealtime(imageDuration);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }
}