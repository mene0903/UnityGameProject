using UnityEngine;
using UnityEngine.SceneManagement;

public class MedicineInstantDeath : MonoBehaviour
{
    [Header("플레이어 태그")]
    public string playerTag = "Player";

    [Header("플레이어 Animator")]
    public Animator playerAnimator;

    [Header("부품 먹은 후 Animator Controller")]
    public RuntimeAnimatorController afterAnimator;

    [Header("부품 먹기 전 의약품 죽음 씬")]
    public string beforePartDeathSceneName;

    [Header("부품 먹은 후 의약품 죽음 씬")]
    public string afterPartDeathSceneName;

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered)
            return;

        if (!other.CompareTag(playerTag))
            return;

        isTriggered = true;
        LoadDeathScene();
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