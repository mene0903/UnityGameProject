using UnityEngine;
using UnityEngine.SceneManagement;

public class ToxicArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die(collision.gameObject);
        }
    }

    void Die(GameObject player)
    {
        Debug.Log("플레이어 사망");

        // 플레이어 비활성화
        player.SetActive(false);

        // 현재 씬 다시 시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}