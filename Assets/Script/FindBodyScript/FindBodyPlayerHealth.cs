using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 추가

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
    }

    void FindHearts()
    {
        if (hearts == null || hearts.Length == 0 || hearts[0] == null)
        {
            hearts = new Image[]
            {
                GameObject.Find("Heart1")?.GetComponent<Image>(),
                GameObject.Find("Heart2")?.GetComponent<Image>(),
                GameObject.Find("Heart3")?.GetComponent<Image>()
            };
        }
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
            Time.timeScale = 1f; // 혹시 정지 상태면 초기화
            SceneManager.LoadScene("GameOverScene1"); // GameOverScene으로 이동
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = i < currentHealth;
        }
    }
}