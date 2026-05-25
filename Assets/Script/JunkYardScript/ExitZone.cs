using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitZone : MonoBehaviour
{
    public string nextSceneName = "FindBodyScene";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(nextSceneName);
    }
}