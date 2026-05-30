using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnRestartButton()
    {
        SceneManager.LoadScene("JunkYardScene"); // 나중에 씬 이름 바꾸기
    }
}