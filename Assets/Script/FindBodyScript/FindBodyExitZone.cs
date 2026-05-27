using UnityEngine;
using UnityEngine.SceneManagement;

public class FindBodyExitZone : MonoBehaviour
{
    public string nextSceneName = "NoArmLegScene";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!GameStateManager.Instance.hasFoundPart)
        {
            Debug.Log("부품을 먼저 찾아야 합니다!");
            return;
        }

        if (!GameStateManager.Instance.hasFoundKey)
        {
            Debug.Log("열쇠를 먼저 찾아야 합니다!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}