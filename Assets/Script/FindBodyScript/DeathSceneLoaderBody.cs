using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathSceneLoaderBody : MonoBehaviour
{
    [Header("ÇÏÆ® ÀÌ¹ÌÁöµé")]
    public Image[] hearts;
    [Header("¸öÅë Ã£±â Àü Á×À½ ¾À")]
    public string deathSceneBeforePart = "DeathCutscene_HeadOnly";
    [Header("¸öÅë Ã£Àº ÈÄ Á×À½ ¾À")]
    public string deathSceneAfterPart = "DeathCutscene_WithBody";
    private bool isLoading = false;
    void Update()
    {
        if (isLoading) return;
        if (AllHeartsGone())
        {
            isLoading = true;
            if (GameStateManager.Instance != null && GameStateManager.Instance.hasFoundPart)
            {
                SceneManager.LoadScene(deathSceneAfterPart);
            }
            else
            {
                SceneManager.LoadScene(deathSceneBeforePart);
            }
        }
    }
    bool AllHeartsGone()
    {
        if (hearts == null || hearts.Length == 0)
            return false;
        foreach (Image heart in hearts)
        {
            if (heart != null && heart.enabled)
                return false;
        }
        return true;
    }
}