using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindBodyPlayerHealth : MonoBehaviour
{
    [Header("하트 UI")]
    public Image[] hearts;
    [Header("체력")]
    public int maxHealth = 3;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        FindHearts();
    }

    void FindHearts()
    {
        // 조건 없이 항상 다시 찾기
        hearts = new Image[]
        {
        GameObject.Find("Heart1")?.GetComponent<Image>(),
        GameObject.Find("Heart2")?.GetComponent<Image>(),
        GameObject.Find("Heart3")?.GetComponent<Image>()
        };
    }

    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        FindHearts();
        UpdateHearts();
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth < 0) currentHealth = 0;
        FindHearts();
        UpdateHearts();
        if (currentHealth <= 0)
        {
            Debug.Log("플레이어 사망");
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameOverScene1");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = i < currentHealth;
            else
                Debug.LogWarning("하트 " + i + "번이 연결 안 됨");
        }
    }
}