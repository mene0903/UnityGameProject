using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthDeathSceneLoader : MonoBehaviour
{
    [Header("ÇÏÆ® UI")]
    public Image[] hearts;

    [Header("ÇÃ·¹ÀÌ¾î Animator")]
    public Animator playerAnimator;

    [Header("ºÎÇ° ¸ÔÀº ÈÄ Animator Controller")]
    public RuntimeAnimatorController afterAnimator;

    [Header("ºÎÇ° ¸Ô±â Àü Á×À½ ¾À")]
    public string beforePartDeathSceneName;

    [Header("ºÎÇ° ¸ÔÀº ÈÄ Á×À½ ¾À")]
    public string afterPartDeathSceneName;

    [Header("¾À ÀüÈ¯ µô·¹ÀÌ")]
    public float delay = 0.5f;

    private bool isDead = false;

    void Update()
    {
        if (isDead)
            return;

        if (AreAllHeartsOff())
        {
            isDead = true;
            Invoke(nameof(LoadDeathScene), delay);
        }
    }

    bool AreAllHeartsOff()
    {
        if (hearts == null || hearts.Length == 0)
            return false;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null && hearts[i].enabled)
                return false;
        }

        return true;
    }

    void LoadDeathScene()
    {
        if (playerAnimator == null)
        {
            SceneManager.LoadScene(beforePartDeathSceneName);
            return;
        }

        RuntimeAnimatorController currentAnimator =
            playerAnimator.runtimeAnimatorController;

        Debug.Log("ÇöÀç Animator Controller: " + currentAnimator.name);
        Debug.Log("ºÎÇ° ¸ÔÀº ÈÄ Animator Controller: " + afterAnimator.name);

        if (currentAnimator == afterAnimator)
        {
            SceneManager.LoadScene(afterPartDeathSceneName);
        }
        else
        {
            SceneManager.LoadScene(beforePartDeathSceneName);
        }
    }
}