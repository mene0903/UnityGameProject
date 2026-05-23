using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour
{
    public string nextSceneName = "JunkYardScene";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "머리_정면")
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
